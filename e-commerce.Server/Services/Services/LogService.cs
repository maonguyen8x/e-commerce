using e_commerce.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.Data;
using e_commerce.Server.DTO.Log;

namespace e_commerce.Server.Services.Services
{
    public class LogService : ILogService
    {
        #region Constructor & DI
        private readonly DataContext _context;

        public LogService(DataContext context)
        {
            _context = context;
        }
        #endregion

        #region SaveNewLog
        public async Task SaveNewLog(string UserName, string Description)
        {
            var newLog = new Log()
            {
                UserName = UserName,
                Description = Description
            };

            await _context.Logs.AddAsync(newLog);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region GetLogsAsync
        public async Task<IEnumerable<GetLogDto>> GetLogsAsync()
        {
            var logs = await _context.Logs
                 .Select(q => new GetLogDto
                 {
                     CreatedAt = q.CreatedAt,
                     Description = q.Description,
                     UserName = q.UserName,
                 })
                 .OrderByDescending(q => q.CreatedAt)
                 .ToListAsync();
            return logs;
        }
        #endregion

        #region GetMyLogsAsync
        public async Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal User)
        {
            var logs = await _context.Logs
                .Where(q => q.UserName == User.Identity.Name)
               .Select(q => new GetLogDto
               {
                   CreatedAt = q.CreatedAt,
                   Description = q.Description,
                   UserName = q.UserName,
               })
               .OrderByDescending(q => q.CreatedAt)
               .ToListAsync();
            return logs;
        }
        #endregion

    }
}
