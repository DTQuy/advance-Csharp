using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Response.AppVersion;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.Service;
using advance_Csharp.Service.Interface;
using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IApplicationService _ApplicationService;

        public ProductController()
        {
            _ApplicationService = new ApplicationService();
        }

        [Route("get-product-user")]
        [HttpGet()]
        [MyAppAuthentication("User")]
        public async Task<IActionResult> GetProduct([FromQuery] ProductGetListRequest request)
        {
            try
            {
                ProductGetListResponse response = await _ApplicationService.GetApplicationProductList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // send to logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("get-product-admin")]
        [HttpGet()]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> GetProductAdmin([FromQuery] ProductGetListRequest request)
        {
            try
            {
                ProductGetListResponse response = await _ApplicationService.GetApplicationProductList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // send to logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// create-product-admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("create-product-admin")]
        [HttpPost]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            try
            {
                Product newProduct = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    Unit = request.Unit,
                    Images = request.Images,
                    Category = request.Category
                };

                using (AdvanceCsharpContext context = new AdvanceCsharpContext())
                {
                    // Thêm sản phẩm mới vào cơ sở dữ liệu
                    context.Products.Add(newProduct);
                    await context.SaveChangesAsync();
                }

                // Tạo DTO cho thông tin sản phẩm
                var productResponse = new ProductResponse

                {
                    Id = newProduct.Id,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Quantity = newProduct.Quantity,
                    Unit = newProduct.Unit,
                    Images = newProduct.Images,
                    Category = newProduct.Category
                };

                // Tạo DTO cho response
                var response = new ProductCreateResponse
                {
                    Message = "Product created successfully",
                    productResponse = productResponse
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc gửi lỗi đến một dịch vụ log
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// update-product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update-product-admin")]
        [HttpPut]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest request)
        {
            try
            {
                // Kiểm tra xem request có hợp lệ không
                if (!request.IsPriceValid)
                {
                    return BadRequest("Invalid Price format. Please enter a valid number.");
                }

                using (AdvanceCsharpContext context = new AdvanceCsharpContext())
                {
                    // Kiểm tra xem sản phẩm có tồn tại không
                    var existingProduct = await context.Products.FindAsync(request.Id);
                    if (existingProduct == null)
                    {
                        return NotFound("Product not found");
                    }

                    // Lưu thông tin cũ của sản phẩm
                    var oldProduct = new ProductResponse
                    {
                        Id = existingProduct.Id,
                        Name = existingProduct.Name,
                        Price = existingProduct.Price,
                        Quantity = existingProduct.Quantity,
                        Unit = existingProduct.Unit,
                        Images = existingProduct.Images,
                        Category = existingProduct.Category
                    };

                    // Cập nhật thông tin sản phẩm
                    existingProduct.Name = request.Name;
                    existingProduct.Price = request.Price;
                    existingProduct.Quantity = request.Quantity;
                    existingProduct.Unit = request.Unit;
                    existingProduct.Images = request.Images;
                    existingProduct.Category = request.Category;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    // Tạo DTO cho thông tin sản phẩm sau khi cập nhật
                    var updatedProduct = new ProductResponse
                    {
                        Id = existingProduct.Id,
                        Name = existingProduct.Name,
                        Price = existingProduct.Price,
                        Quantity = existingProduct.Quantity,
                        Unit = existingProduct.Unit,
                        Images = existingProduct.Images,
                        Category = existingProduct.Category
                    };

                    // Tạo DTO cho response
                    var response = new ProductUpdateResponse
                    {
                        Message = "Product updated successfully",
                        OldProduct = oldProduct,
                        UpdatedProduct = updatedProduct
                    };

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc gửi lỗi đến một dịch vụ log
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete-product-admin")]
        [HttpDelete]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                using (AdvanceCsharpContext context = new AdvanceCsharpContext())
                {
                    // Kiểm tra xem sản phẩm có tồn tại không
                    var existingProduct = await context.Products.FindAsync(id);
                    if (existingProduct == null)
                    {
                        return NotFound("Product not found");
                    }

                    // Lưu thông tin sản phẩm cũ
                    var deletedProduct = new ProductResponse
                    {
                        Id = existingProduct.Id,
                        Name = existingProduct.Name,
                        Price = existingProduct.Price,
                        Quantity = existingProduct.Quantity,
                        Unit = existingProduct.Unit,
                        Images = existingProduct.Images,
                        Category = existingProduct.Category
                    };

                    // Xóa sản phẩm
                    context.Products.Remove(existingProduct);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    // Trả về thông báo thành công và thông tin của sản phẩm đã xóa
                    var response = new ProductDeleteResponse("Product deleted successfully", deletedProduct);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc gửi lỗi đến một dịch vụ log
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
