using e_commerce.Server.DTO.Auth;
using e_commerce.Server.DTO.Response;
using e_commerce.Server.DTO.Accounts;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IAuthService
    {
        //Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers();
        //Task<ServiceResponse<GetUserDTO>> GetUserById(int id);
        Task<GeneralServiceResponseDto> SeedRolesAsync();
        Task<GeneralServiceResponseDto> RegisterUserAsync(RegisterDto newUser);
        Task<GeneralServiceResponseDto?> LoginAsync(LoginDTO request);
        Task<LoginServiceResponseDTO?> MeAsync(MeDTO meDto);
        Task<IEnumerable<string>> GetByUsernameAsync(string username);
        Task<(bool Success, string Message)> RegisterAdminAsync(RegisterDto model);
        //Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO updatedProduct);
        //Task<ServiceResponse<List<GetUserDTO>>> DeleteUser(int id);
    }
}
