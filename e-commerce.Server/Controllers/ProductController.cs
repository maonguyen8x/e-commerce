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
        //[Authorize(Roles = StaticUserRoles.OwnerAdmin)]
        public async Task<ActionResult<IEnumerable<GetAllProductsNewDTO>>> GetAllProducts()
        {
            //var messages = await _productService.GetAllProducts();
            var seedData = new List<GetAllProductsNewDTO>();
            seedData.Add(new GetAllProductsNewDTO()
            {
                AvailableColors = new List<string> { "#fbbcbc", "#171616", "#f02424" },
                Brand = "Apple",
                DateAdded = 1726191550570,
                Description = "Apple、iPhone 16 ProとiPhone 16 Pro Maxを発表 A18 Proチップを搭載し、Apple Intelligenceのために設計されたProのラインナップは、一段と大きいディスプレイサイズ、カメラコントロール、革新的なプロ向けのカメラ機能、飛躍的に向上したバッテリー駆動時間を実現します",
                Image = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FeCL5mi5SXWuoMVrmj6Kp?alt=media&token=e1e5af37-83c7-4154-8f81-a2fd78f753ef",
                ImageCollection = new List<ImageCollectionItem>
        {
            new ImageCollectionItem { Id = "eCL5mi5SXWuoMVrmj6Kp", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FeCL5mi5SXWuoMVrmj6Kp?alt=media&token=b0156da0-ef64-4c3f-8635-767e145ff40f" },
            new ImageCollectionItem { Id = "Wl9fB87ndpe9gzzZJE76", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FfVQCMLL7n7kZviUiybet?alt=media&token=3d3d002c-4078-4604-b72a-4d97c8516fcf" }
        },
                IsFeatured = true,
                IsRecommended = false,
                Keywords = new List<string> { "Iphone", "Iphone 16 pro" },
                MaxQuantity = 240000,
                Name = "Iphone 16 Pro",
                NameLower = "iphone 16 pro",
                Price = 201800,
                Quantity = 1,
                Sizes = new List<string> { "100" }
            });
            seedData.Add(new GetAllProductsNewDTO()
            {
                AvailableColors = new List<string> { "#403f3f", "#b14e4e", "#3b56de" },
                Brand = "Apple",
                DateAdded = 1728006542328,
                Description = "124,800円（税込）から、または3,466円/月（税込）の36回払いから* 予約注文は9月13日午後9時から 9月20日発売 Apple Intelligenceは年内に米国英語から公開予定1",
                Image = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2Fl4SID2IapZ7dJkhSts2h?alt=media&token=ead8dd2e-5f14-49a2-975f-5df06b302a2e",
                ImageCollection = new List<ImageCollectionItem>
        {
            new ImageCollectionItem { Id = "l4SID2IapZ7dJkhSts2h", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2Fl4SID2IapZ7dJkhSts2h?alt=media&token=bef81524-3c52-4ce2-b70d-2f8685125a3b" },
            new ImageCollectionItem { Id = "exwsVmBdufgO0qSA98cI", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FTfosyBU3OT0kBN1pOyXO?alt=media&token=614c075d-3786-4ac1-acae-ffaf3af4329f" }
        },
                IsFeatured = true,
                IsRecommended = true,
                Keywords = new List<string> { "Iphone 16 Plus", "Apple" },
                MaxQuantity = 134576,
                Name = "Iphone 16 Plus ",
                NameLower = "iphone 16 plus ",
                Price = 123453,
                Quantity = 1,
                Sizes = new List<string> { "123" }
            });
            seedData.Add(new GetAllProductsNewDTO()
            {
                AvailableColors = new List<string> { "#1a1919", "#dd4b4b", "#421ef6" },
                Brand = "Apple",
                DateAdded = 1726191853947,
                Description = "Appleの環境に関する取り組み。 Appleは、2030年までにカーボンニュートラルを達成するための取り組みを続けています。その一環として、iPhone 15とiPhone 15 Plusには電源アダプタとEarPodsが付属していません。製品には、USB‑C電源アダプタとコンピュータのポートに対応し、高速充電ができるUSB‑C充電ケーブルが同梱されています。 Appleでは、今お持ちのUSB‑C電源アダプタを使用することをおすすめします。必要に応じて、新しいApple電源アダプタやヘッドフォンを購入することもできます。",
                Image = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2Fq1ba0Tmvdw0PfUfsOA01?alt=media&token=0ffc3427-fe54-44da-9480-4e6bee84e147",
                ImageCollection = new List<ImageCollectionItem>
        {
            new ImageCollectionItem { Id = "q1ba0Tmvdw0PfUfsOA01", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2Fq1ba0Tmvdw0PfUfsOA01?alt=media&token=0ffc3427-fe54-44da-9480-4e6bee84e147" },
            new ImageCollectionItem { Id = "ciFV7jjaCyVh2Wc5mdQo", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FP7KLvl6wjhV66FMj3SX2?alt=media&token=42499243-0648-4a64-aa42-bcde4519f9c6" }
        },
                IsFeatured = true,
                IsRecommended = true,
                Keywords = new List<string> { "Iphone 15 Pro", "Iphone", "Apple" },
                MaxQuantity = 135000,
                Name = "Iphone 15 Pro ",
                NameLower = "iphone 15 pro ",
                Price = 124800,
                Quantity = 1,
                Sizes = new List<string> { "120" }
            });
            seedData.Add(new GetAllProductsNewDTO()
            {
                AvailableColors = new List<string> { "#272525", "#f33535", "#1777de" },
                Brand = "Apple",
                DateAdded = 1726208543666,
                Description = "100％の機能が保証されています 専門家による業界基準で最多の25項目以上の検品、購入から1年間の動作保証が付いています。 新品同様 外装から内部まで、クリーニングと検品が繰り返された最良の状態です。 全機能の動作確認済み セキュリティの安全性が確保された、安心して使えるSIMフリーのデバイスのみ販売。",
                Image = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FshjuwYX1Y0UXS2EsRD3b?alt=media&token=3a44c60e-0c2a-4c16-9c82-36b61d013523",
                ImageCollection = new List<ImageCollectionItem>
        {
            new ImageCollectionItem { Id = "shjuwYX1Y0UXS2EsRD3b", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FshjuwYX1Y0UXS2EsRD3b?alt=media&token=3a44c60e-0c2a-4c16-9c82-36b61d013523" },
            new ImageCollectionItem { Id = "22Zgl3XcplULcQ3ZTW0g", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FURZgfBkgVvVpUBd9hYGg?alt=media&token=c4d21129-ff85-4381-89c3-4fca19f4dd77" }
        },
                IsFeatured = true,
                IsRecommended = true,
                Keywords = new List<string> { "Iphone", "Apple" },
                MaxQuantity = 444444,
                Name = "Iphone 14 plus",
                NameLower = "iphone 14 plus",
                Price = 234455,
                Quantity = 1,
                Sizes = new List<string> { "12" }
            });
            seedData.Add(new GetAllProductsNewDTO()
            {
                AvailableColors = new List<string> { "#504949" },
                Brand = "Apple",
                DateAdded = 1726191678937,
                Description = "12コアCPU 18コアGPU 18GBユニファイドメモリ 512GB SSDストレージ¹ 16インチLiquid Retina XDRディスプレイ² Thunderbolt 4ポート x 3、HDMIポート、SDXCカードスロット、ヘッドフォンジャック、MagSafe 3ポート Touch ID搭載Magic Keyboard 感圧タッチトラックパッド 140W USB-C電源アダプタ",
                Image = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2Fy7KnXl7aSUJO0sdlFeHu?alt=media&token=19afc6f0-f389-4c47-8f2c-444a10c86d65",
                ImageCollection = new List<ImageCollectionItem>
        {
            new ImageCollectionItem { Id = "y7KnXl7aSUJO0sdlFeHu", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2Fy7KnXl7aSUJO0sdlFeHu?alt=media&token=723fa8e3-c65a-4fb3-ac38-b49d56b6ede0" },
            new ImageCollectionItem { Id = "wM35N8RMK3lfusvdIXP1", Url = "https://firebasestorage.googleapis.com/v0/b/e-commerce-f3cae.appspot.com/o/products%2FBj2pfAczjwBjVopLMAgV?alt=media&token=672de7c2-2246-4b6a-82cb-dd000641537a" }
        },
                IsFeatured = true,
                IsRecommended = true,
                Keywords = new List<string> { "Macbook Pro M2", "Macbook", "Apple" },
                MaxQuantity = 350000,
                Name = "Macbook Pro M2",
                NameLower = "macbook pro m2",
                Price = 320000,
                Quantity = 1,
                Sizes = new List<string> { "120" }
            });
            return seedData;
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
