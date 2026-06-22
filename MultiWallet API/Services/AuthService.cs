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
        public AuthService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
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

            string decoded_key = "secret_key";
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
                return Result<UserLogInResponseDTO>.Error("Ошибка: неверный логин");

            bool IsOk = BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password, user.PasswordHash);

            if(!IsOk)
                return Result<UserLogInResponseDTO>.Error("Ошибка: неверный пароль");

            string AccessToken = AppendCookiesAndGetAccessToken(user);

            try
            {
                await _context.SaveChangesAsync();
                return Result<UserLogInResponseDTO>.Success(new UserLogInResponseDTO(AccessToken, user));
            }
            catch (Exception ex)
            {
                return Result<UserLogInResponseDTO>.Error("Ошибка: что-то пошло не так");
            }
        }

        public async Task<Result<UserRegistrationResponseDTO>> RegistrAsync(UserRegistrationandLogInRequestDTO loginDTO) // registration
        {
            bool LoginExists = await _context.Users
                .AnyAsync(x => x.Login == loginDTO.Login);

            if (LoginExists)
                return Result<UserRegistrationResponseDTO>.Error("Логин уже занят");

            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(loginDTO.Password);

            var user = new User(loginDTO.Login, hashedPassword);

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                return Result<UserRegistrationResponseDTO>.Success(new UserRegistrationResponseDTO(true, user.Login));
            }
            catch (Exception ex)
            {
                return Result<UserRegistrationResponseDTO>.Error("Ошибка: что-то пошло не так");
            }
        }

        public async Task<Result<UserLogInResponseDTO>> RefreshAsync() // refreshing refreshToken
        {
            var existingRefreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(existingRefreshToken))
                return Result<UserLogInResponseDTO>.Error("Куки пусты");

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == existingRefreshToken);

            if (user == null)
                return Result<UserLogInResponseDTO>.Error("Пользователь не найден");

            if (user.RefreshTokenExpiresAt != null)
            {
                if (user.RefreshTokenExpiresAt < DateTime.UtcNow)
                    return Result<UserLogInResponseDTO>.Error("Сессия истекла");
            }
            else
                return Result<UserLogInResponseDTO>.Error("Ошибка сессии");

            string AccessToken = AppendCookiesAndGetAccessToken(user);

            try
            {
                await _context.SaveChangesAsync();
                return Result<UserLogInResponseDTO>.Success(new UserLogInResponseDTO(AccessToken, user));
            }
            catch (Exception ex)
            {
                return Result<UserLogInResponseDTO>.Error("Что-то пошло не так");
            }
        }
    }
}