using e_commerce.Server.DTO.Log;
using System.Security.Claims;

namespace e_commerce.Server.Services.Interfaces
{
    public interface ILogService
    {
        Task SaveNewLog(string UserName, string Description);
        Task<IEnumerable<GetLogDto>> GetLogsAsync();
        Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal User);

    }
}
