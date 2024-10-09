using e_commerce.Server.DTO.Product;
using e_commerce.Server.Models;
using System.Security.Claims;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDTO>> GetAllProducts();
        Task<ServiceResponse<List<GetProductDTO>>> DeleteProduct(int id);
        Task<ServiceResponse<List<AddProductDTO>>> AddNewProduct(ClaimsPrincipal User, AddProductDTO newProduct);

        //Task<ProductDTO> GetProductByIdAsync(int id);
        //Task<ProductDTO> GetProductByNameAsync(string name);
        //Task AddProductAsync(ProductDTO ProductDTO);
        //Task UpdateProductAsync(ProductDTO ProductDTO);
        //Task DeleteProductAsync(int id);
    }
}

