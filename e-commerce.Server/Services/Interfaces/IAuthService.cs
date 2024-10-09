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
        Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto newUser);
        Task<LoginServiceResponseDTO?> LoginAsync(LoginDTO request);
        Task<LoginServiceResponseDTO?> MeAsync(MeDTO meDto);
        Task<IEnumerable<string>> GetUsernamesListAsync();
        //Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO updatedProduct);
        //Task<ServiceResponse<List<GetUserDTO>>> DeleteUser(int id);
    }
}
