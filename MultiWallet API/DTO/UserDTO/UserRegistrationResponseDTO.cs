namespace _user_registration_response_dto
{
    public class UserRegistrationResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string? Login { get; set; }
        public UserRegistrationResponseDTO(bool IsSuccess, string? Login)
        {
            this.IsSuccess = IsSuccess;
            this.Login = Login;
        }
    }
}