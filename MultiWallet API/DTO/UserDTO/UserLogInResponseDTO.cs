using _user;
namespace _user_login_response_dto
{
    public class UserLogInResponseDTO
    {
        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public Ulid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public UserLogInResponseDTO(string accessToken, User user)
        {
            AccessToken = accessToken;
            RefreshToken = user.RefreshToken;
            Id = user.Id;
            Login = user.Login;
            Name = user.Name;
        }
    }
}