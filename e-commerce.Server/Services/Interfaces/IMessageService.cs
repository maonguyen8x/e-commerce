using e_commerce.Server.DTO.Message;
using e_commerce.Server.DTO.Response;
using System.Security.Claims;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IMessageService
    {
        Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal User, CreateMessageDTO createMessageDto);
        Task<IEnumerable<GetMessageDTO>> GetMessagesAsync();
        Task<IEnumerable<GetMessageDTO>> GetMyMessagesAsync(ClaimsPrincipal User);
    }
}

