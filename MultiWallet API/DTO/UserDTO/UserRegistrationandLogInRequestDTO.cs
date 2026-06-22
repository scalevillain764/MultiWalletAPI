namespace _user_registration_login_request_dto
{
    public class UserRegistrationandLogInRequestDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRegistrationandLogInRequestDTO(string Login, string Password)
        {
            this.Password = Password;
            this.Login = Login;
        }
    }
}