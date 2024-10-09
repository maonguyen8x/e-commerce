using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Server.Constants;
using e_commerce.Server.DTO.Product;
using e_commerce.Server.Services.Interfaces;

namespace e_commerce.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly int MAX_BYTES = 5 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPE = { ".jpg", ".png" };

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        ///// <summary>
        ///// GetAll: Get all products in the database
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GetProductDTO>>> GetAll()
        //{
        //    var productsDto = await _productService.GetAllProducts();
        //    return Ok(productsDto);
        //}

        /// <summary>
        /// AddProduct: Add product by admin role
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("create")]
        [Authorize()]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDTO newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (image == null || image.Length == 0) return BadRequest("Please select a file.");
            //if (image.Length > MAX_BYTES) return BadRequest("File size exceeds the maximum limit.");
            //if (ACCEPTED_FILE_TYPE.Any(a => a == Path.GetExtension(image.FileName).ToLower())) return BadRequest("Invalid file type");

            var result = await _productService.AddNewProduct(User, newProduct);

            if (result.Success)
                return Ok(result.Message);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = StaticUserRoles.OwnerAdmin)]
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> GetAllProducts()
        {
            var messages = await _productService.GetAllProducts();
            return Ok(messages);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetByIdAsync(int id)
        //{
        //    var productDto = await _productService.GetProductByIdAsync(id);
        //    return productDto != null ? Ok(productDto) : NotFound();
        //}

        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetByName(string name)
        //{
        //    var productDto = await _productService.GetProductByNameAsync(name);
        //    return productDto == null ? NotFound() : Ok(productDto);
        //}

        //[HttpPost("create")]
        //public async Task<IActionResult> Add([FromBody] ProductDTO productDto)
        //{
        //    await _productService.AddProductAsync(productDto);
        //    return CreatedAtAction("GetByIdAsync", new { id = productDto.Id }, productDto);
        //}

        //[HttpPut("update/{id}")]
        //public async Task<IActionResult> Update([FromBody] ProductDTO productDto)
        //{
        //    await _productService.UpdateProductAsync(productDto);
        //    return Ok(productDto);
        //}

        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> DeleteById(int id)
        //{
        //    await _productService.GetProductByIdAsync(id);
        //    return NoContent();
        //}
    }

}
