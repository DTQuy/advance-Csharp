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
    /// <summary>
    ///  Controller Api product 
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {       
        private IApplicationService _ApplicationService;

        /// <summary>
        /// Product Controller
        /// </summary>
        public ProductController()
        {
            _ApplicationService = new ApplicationService();
        }

        /// <summary>
        /// get-product-user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get-product-admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                    // Add new products to the database
                    context.Products.Add(newProduct);
                    await context.SaveChangesAsync();
                }

                // Create DTO for product information              
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

                // Create DTO for response
                var response = new ProductCreateResponse
                {
                    Message = "Product created successfully",
                    productResponse = productResponse
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
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
                // Check if the request is valid
                if (!request.IsPriceValid)
                {
                    return BadRequest("Invalid Price format. Please enter a valid number.");
                }

                using (AdvanceCsharpContext context = new AdvanceCsharpContext())
                {
                    // Check if the product exists
                    var existingProduct = await context.Products.FindAsync(request.Id);
                    if (existingProduct == null)
                    {
                        return NotFound("Product not found");
                    }

                    // Save old product information
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

                    // Update product information
                    existingProduct.Name = request.Name;
                    existingProduct.Price = request.Price;
                    existingProduct.Quantity = request.Quantity;
                    existingProduct.Unit = request.Unit;
                    existingProduct.Images = request.Images;
                    existingProduct.Category = request.Category;

                    // Save changes to the database
                    await context.SaveChangesAsync();

                    // Generate DTO for product information after update
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

                    // Create DTO for response
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
                // Log errors or send errors to a logging service
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
                    // Check if the product exists
                    var existingProduct = await context.Products.FindAsync(id);
                    if (existingProduct == null)
                    {
                        return NotFound("Product not found");
                    }

                    // Save old product information
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

                    // Delete product
                    context.Products.Remove(existingProduct);

                    // Save changes to the database
                    await context.SaveChangesAsync();

                    // Returns a success message and information about the deleted product
                    var response = new ProductDeleteResponse("Product deleted successfully", deletedProduct);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
