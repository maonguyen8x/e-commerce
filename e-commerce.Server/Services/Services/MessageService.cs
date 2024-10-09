using e_commerce.Server.DTO.Message;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using e_commerce.Server.Data;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.DTO.Response;

namespace e_commerce.Server.Services.Services
{
    public class MessageService : IMessageService
    {
        #region Constructor & DI
        private readonly DataContext _context;
        private readonly ILogService _logService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageService(DataContext context, ILogService logService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logService = logService;
            _userManager = userManager;
        }
        #endregion

        #region CreateNewMessageAsync
        public async Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal User, CreateMessageDTO createMessageDto)
        {
            if (User.Identity.Name == createMessageDto.ReceiverUserName)
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = false,
                    StatusCode = 400,
                    Message = "Sender and Receiver can not be same",
                };

            var isReceiverUserNameValid = _userManager.Users.Any(q => q.UserName == createMessageDto.ReceiverUserName);
            if (!isReceiverUserNameValid)
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = false,
                    StatusCode = 400,
                    Message = "Receiver UserName is not valid",
                };

            Message newMessage = new Message()
            {
                SenderUserName = User.Identity.Name,
                ReceiverUserName = createMessageDto.ReceiverUserName,
                Text = createMessageDto.Text
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            await _logService.SaveNewLog(User.Identity.Name, "Send Message");

            return new GeneralServiceResponseDto()
            {
                IsSucceed = true,
                StatusCode = 201,
                Message = "Message saved successfully",
            };
        }
        #endregion

        #region GetMessagesAsync
        public async Task<IEnumerable<GetMessageDTO>> GetMessagesAsync()
        {
            var messages = await _context.Messages
                .Select(q => new GetMessageDTO()
                {
                    Id = q.Id,
                    SenderUserName = q.SenderUserName,
                    ReceiverUserName = q.ReceiverUserName,
                    Text = q.Text,
                    CreatedAt = q.CreatedAt
                })
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            return messages;
        }
        #endregion

        #region GetMyMessagesAsync
        public async Task<IEnumerable<GetMessageDTO>> GetMyMessagesAsync(ClaimsPrincipal User)
        {
            var loggedInUser = User.Identity.Name;

            var messages = await _context.Messages
                .Where(q => q.SenderUserName == loggedInUser || q.ReceiverUserName == loggedInUser)
             .Select(q => new GetMessageDTO()
             {
                 Id = q.Id,
                 SenderUserName = q.SenderUserName,
                 ReceiverUserName = q.ReceiverUserName,
                 Text = q.Text,
                 CreatedAt = q.CreatedAt
             })
             .OrderByDescending(q => q.CreatedAt)
             .ToListAsync();

            return messages;
        }
        #endregion
    }
}


