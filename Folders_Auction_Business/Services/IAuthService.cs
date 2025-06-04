using Folders_Auction_Core.DTOs;

namespace Folders_Auction_Business.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
