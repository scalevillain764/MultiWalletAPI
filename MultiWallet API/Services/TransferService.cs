using _tranfser_response_dto;
using _transfer_creation_dto;
using _interfaces;
using _context;
namespace _transfer_service
{
    public class TransferService : ITransferService
    {
        private readonly AppDbContext _context;
        public TransferService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<Results<TransferResponseDTO>> MakeTransfer(Ulid UserId, TransferCreationDTO transferCreationDTO)
        {
            var fromWallet = await _context.
        }
    }
}