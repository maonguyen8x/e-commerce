using e_commerce.Server.Data;
using e_commerce.Server.DTO.Product;
using e_commerce.Server.Models;
using Microsoft.EntityFrameworkCore;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using e_commerce.Server.DTO.Response;

namespace e_commerce.Server.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogService _logService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(
            DataContext context, 
            ILogService logService, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logService = logService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GetProductDTO>> GetAllProducts()
        {
            var serviceResponse = new GeneralServiceResponseDto();
            var products = await _context.Products
                 .Select(q => new GetProductDTO
                 {
                     Name = q.Name,
                     UserName = q.UserName,
                     Color = q.Color,
                     Description = q.Description,
                     //Image = q.Image,
                     Price = q.Price,
                     Quality = q.Quality,
                     CreatedAt = q.CreatedAt,
                 })
                 .OrderByDescending(q => q.CreatedAt)
                 .ToListAsync();
            try
            {
                serviceResponse.IsSucceed = false;
                serviceResponse.Message = "Get products successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.IsSucceed = false;
                serviceResponse.Message = ex.Message;
            }

            return products;
        }

        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<AddProductDTO>>> AddNewProduct(ClaimsPrincipal User, AddProductDTO newProduct)
        {
            var serviceResponse = new ServiceResponse<List<AddProductDTO>>();

            try
            {
                // Save image to storage
                //string imageUrl = await SaveImageToLocalStorage(image);

                Product product = new Product()
                {
                    UserName = User.Identity.Name,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Color = newProduct.Color,
                    Quality = newProduct.Quality,
                    Description = newProduct.Description,
                    //Image = imageUrl,
                };
                
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return new ServiceResponse<List<AddProductDTO>>()
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Product saved successfully",
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private async Task<string> SaveImageToLocalStorage(IFormFile image)
        {
            var rootFolder = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            try
            {
                string uploadsFolder = Path.Combine(rootFolder, "uploads/");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                //return new ImageUploadResult { Success = false, Message = ex.Message });
            }

            return $"uploads/{uniqueFileName}";

        }

        public async Task<ServiceResponse<List<GetProductDTO>>> DeleteProduct(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDTO>>();

            try
            {
                //var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                //if (product is null)
                //    throw new Exception($"Product with Id '{id}' not found.");

                //_context.Products.Remove(product);
                //await _context.SaveChangesAsync();
                //serviceResponse.Data = await _context.Products.Select(p => _mapper.Map<GetProductDTO>(p)).ToListAsync();
                //serviceResponse.Message = "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        /// <summary>
        /// GetProductById: Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseDto<GetProductDTO>> GetProductById(int id)
        //{
            //var serviceResponse = new ServiceResponseDto<GetProductDTO>();
            //try
            //{
            //    var dbProduct = await _context.Products
            //    .FirstOrDefaultAsync(p => p.Id == id);
            //    serviceResponse.Data = _mapper.Map<GetProductDTO>(dbProduct);
            //}
            //catch (Exception ex)
            //{
            //    serviceResponse.IsSuccess = false;
            //    serviceResponse.Message = ex.Message;
            //}
            //return serviceResponse;
        //}
    }
}
