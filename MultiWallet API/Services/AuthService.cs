using _interfaces;
using _result;
using _user;
using _context;
using _user_registration_response_dto;
using _user_registration_login_request_dto;
using _user_login_response_dto;

using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace _auth_service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        public AuthService(IHttpContextAccessor httpContextAccessor, AppDbContext context, ILogger<AuthService> logger, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        private string AppendCookiesAndGetAccessToken(User user)
        {
            string AccessToken = CreateAccessToken(user.Id, user.Name);
            string RefreshToken = CreateRefreshToken();

            user.RefreshToken = RefreshToken;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,        // защита от XSS атак
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", RefreshToken, cookieOptions);

            return AccessToken;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string CreateAccessToken(Ulid Id, string UserName)
        {
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name, UserName)
            };

            string decoded_key = _configuration["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(decoded_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "MyAuthServer",
                    audience: "MyWalletApp",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Result<UserLogInResponseDTO>> LogInAsync(UserRegistrationandLogInRequestDTO loginDTO) // login
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == loginDTO.Login);

            if (user == null)
                return Result<UserLogInResponseDTO>.Error("Ошибка: неверный логин", Result<UserLogInResponseDTO>.ErrorType.Validation);

            bool IsOk = BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password, user.PasswordHash);

            if(!IsOk)
                return Result<UserLogInResponseDTO>.Error("Ошибка: неверный пароль", Result<UserLogInResponseDTO>.ErrorType.Validation);

            string AccessToken = AppendCookiesAndGetAccessToken(user);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"User {user.Id.ToString()} logged in");
            return Result<UserLogInResponseDTO>.Success(new UserLogInResponseDTO(AccessToken, user));
        }

        public async Task<Result<UserRegistrationResponseDTO>> RegistrAsync(UserRegistrationandLogInRequestDTO loginDTO) // registration
        {
            bool LoginExists = await _context.Users
                .AnyAsync(x => x.Login == loginDTO.Login);

            if (LoginExists)
                return Result<UserRegistrationResponseDTO>.Error("Логин уже занят", Result<UserRegistrationResponseDTO>.ErrorType.Validation);

            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(loginDTO.Password);

            var user = new User(loginDTO.Login, hashedPassword);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {user.Id.ToString()} registred");
            return Result<UserRegistrationResponseDTO>.Success(new UserRegistrationResponseDTO(true, user.Login));
        }

        public async Task<Result<UserLogInResponseDTO>> RefreshAsync() // refreshing refreshToken
        {
            var existingRefreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(existingRefreshToken))
                return Result<UserLogInResponseDTO>.Error("Куки пусты", Result<UserLogInResponseDTO>.ErrorType.Unauthorized);

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == existingRefreshToken);

            if (user == null)
                return Result<UserLogInResponseDTO>.Error("Пользователь не найден", Result<UserLogInResponseDTO>.ErrorType.Unauthorized);

            if (user.RefreshTokenExpiresAt != null)
            {
                if (user.RefreshTokenExpiresAt < DateTime.UtcNow)
                    return Result<UserLogInResponseDTO>.Error("Сессия истекла", Result<UserLogInResponseDTO>.ErrorType.Unauthorized);
            }
            else
                return Result<UserLogInResponseDTO>.Error("Ошибка сессии", Result<UserLogInResponseDTO>.ErrorType.Unauthorized);

            string AccessToken = AppendCookiesAndGetAccessToken(user);

            await _context.SaveChangesAsync();
            return Result<UserLogInResponseDTO>.Success(new UserLogInResponseDTO(AccessToken, user));
        }
    }
}