using _result;
using _user;

// DTO

    // UserDTO
using _user_response_dto;
using _user_registration_response_dto;
using _user_registration_login_request_dto;
using _user_login_response_dto;

    // WalletDTO
using _wallet_creation_dto;
using _wallet_response_dto;

    // Transfer DTO
using _tranfser_response_dto;
using _transfer_creation_dto;

    // Expenses DTO
using _expense_creation_dto;
using _expense_response_dto;

namespace _interfaces
{
    public interface IEntity
    {
        public Ulid Id { get; set; }
    }

    public interface IAuthService
    {
        Task<Result<UserLogInResponseDTO>> LogInAsync(UserRegistrationandLogInRequestDTO loginDTO);
        Task<Result<UserRegistrationResponseDTO>> RegistrAsync(UserRegistrationandLogInRequestDTO loginDTO);
        Task<Result<UserLogInResponseDTO>> RefreshAsync();
        // Remove account function!!!
    }

    public interface IWalletService
    {
        Task<Result<WalletResponseDTO>> AddWalletAsync(Ulid UserId, WalletCreationDTO walletCreationDTO);
        Task<Result<WalletResponseDTO>> RemoveWalletAsync(Ulid UserId, Ulid WalletId);
        Task<Result<WalletResponseDTO>> ChangeWalletNameAsync(Ulid UserId, Ulid WalletId, string NewName);
        Task<Result<WalletResponseDTO>> ReplenishBalanceAsync(Ulid UserId, Ulid WalletId, decimal Amount);
        Task<Result<List<WalletResponseDTO>>> GetAllAsync(Ulid UserId);
    }

    public interface ITransferService
    {
        Task<Result<TransferResponseDTO>> MakeTransfer(Ulid UserId, TransferCreationDTO transferCreationDTO);
    }

    public interface IBudgetService
    {
        Task<Result<ExpenseResponseDTO>> MakeExpense(Ulid UserId, ExpenseCreationDTO expenseCreationDTO);
    }
}