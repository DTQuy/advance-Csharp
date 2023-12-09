using advance_Csharp.dto.Request.Cart;
using advance_Csharp.dto.Response.Cart;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        /// <summary>
        /// view-all-cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("/view-all-cart")]
        [HttpGet()]
        public async Task<ActionResult<List<CartResponse>>> GetAllCarts()
        {
            try
            {
                List<CartResponse> carts = await _cartService.GetAllCarts();
                return Ok(carts);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// ViewCart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("/view-cart")]
        [HttpGet()]
        public async Task<IActionResult> ViewCart(Guid userId)
        {
            try
            {
                CartResponse cartResponse = await _cartService.ViewCart(userId);
                return Ok(cartResponse);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// AddProductToCart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("/add-product")]
        [HttpPost()]
        public async Task<IActionResult> AddProductToCart([FromBody] CartRequest request)
        {
            try
            {
                bool result = await _cartService.AddProductToCart(request);
                return result ? Ok("Product added to the cart successfully.") : BadRequest("Failed to add the product to the cart.");
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// DeleteProductFromCart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("/delete-product")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteProductFromCart(Guid userId, Guid productId)
        {
            try
            {
                bool result = await _cartService.DeleteProductFromCart(userId, productId);
                return result ? Ok("Product deleted from the cart successfully.") : BadRequest("Failed to delete the product from the cart.");
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// UpdateQuantity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Route("api/update-quantity")]
        [HttpPut()]
        public async Task<IActionResult> UpdateQuantity(Guid userId, Guid productId, int quantity)
        {
            try
            {
                bool result = await _cartService.UpdateQuantity(userId, productId, quantity);
                return result ? Ok("Cart quantity updated successfully.") : BadRequest("Failed to update cart quantity.");
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


