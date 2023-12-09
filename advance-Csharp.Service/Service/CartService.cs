using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.Cart;
using advance_Csharp.dto.Response.Cart;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class CartService : ICartService
    {
        private readonly DbContextOptions<AdvanceCsharpContext> _context;
        private readonly IProductService _productService;

        public CartService(DbContextOptions<AdvanceCsharpContext> dbContextOptions, IProductService productService)
        {
            _context = dbContextOptions;
            _productService = productService;
        }

        /// <summary>
        /// GetAllCarts
        /// </summary>
        /// <returns></returns>
        public async Task<List<CartResponse>> GetAllCarts()
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);
                if (context.Carts == null)
                {
                    // Handle the case where context.Carts is null
                    return new List<CartResponse>();
                }

                List<Cart> allCarts = await context.Carts
                    .Include(c => c.CartDetails)
                    .ToListAsync();

                List<CartResponse> cartResponses = allCarts.Select(cart => new CartResponse
                {
                    Message = "Success! The user's cart has a UserId of: " + cart.UserId,
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CartDetails = cart.CartDetails?.Select(cd => new CartDetailResponse
                    {
                        Id = cd.Id,
                        CartId = cd.CartId,
                        ProductId = cd.ProductId,
                        Price = cd.Price,
                        Quantity = cd.Quantity
                    }).ToList() ?? new List<CartDetailResponse>()
                }).ToList();

                return cartResponses;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                Console.WriteLine($"An error occurred while retrieving all carts: {ex.Message}");
                return new List<CartResponse>();
            }
        }

        /// <summary>
        /// GetCartByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CartResponse> GetCartByUserId(Guid userId)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                if (context.Carts == null)
                {
                    // Handle the case where context.Carts is null
                    return new CartResponse
                    {
                        Message = "Error: context.Carts is null."
                    };
                }

                Cart? cart = await context.Carts
                    .Include(c => c.CartDetails)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    // Create an empty cart if it doesn't exist
                    cart = new Cart { UserId = userId };
                    _ = context.Carts.Add(cart);
                    _ = await context.SaveChangesAsync();
                }

                CartResponse cartResponse = new()
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CartDetails = cart.CartDetails?.Select(cd => new CartDetailResponse
                    {
                        Id = cd.Id,
                        CartId = cd.CartId,
                        ProductId = cd.ProductId,
                        Price = cd.Price,
                        Quantity = cd.Quantity
                    }).ToList() ?? new List<CartDetailResponse>()
                };

                return cartResponse;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                return new CartResponse
                {
                    Message = $"An error occurred while retrieving the cart: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// AddProductToCart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddProductToCart(CartRequest request)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                // Check if the user's cart already exists
                Cart? cart = await context.Carts
                    .Include(c => c.CartDetails)
                    .FirstOrDefaultAsync(c => c.UserId == request.UserId);

                // If the cart doesn't exist, create a new cart
                cart ??= new Cart
                {
                    UserId = request.UserId,
                    CartDetails = new List<CartDetail>()
                };

                // Check if the product already exists in the cart
                CartDetail? existingCartDetail = cart?.CartDetails?
                    .FirstOrDefault(cd => cd.ProductId == request.CartDetails?[0]?.ProductId);

                // Check if the product doesn't exist or if CartDetails is null
                if (existingCartDetail != null)
                {
                    // If the product exists, update the quantity
                    existingCartDetail.Quantity += request.CartDetails?[0]?.Quantity ?? 0;
                }
                else if (cart?.CartDetails != null)
                {
                    // If the product doesn't exist, add a new cart detail
                    CartDetail newCartDetail = new()
                    {
                        ProductId = request.CartDetails?[0]?.ProductId ?? Guid.Empty,
                        Quantity = request.CartDetails?[0]?.Quantity ?? 0,
                        // Set the Price based on the product's price
                        Price = await GetProductPriceAsync(request.CartDetails?[0]?.ProductId ?? Guid.Empty)
                    };

                    cart.CartDetails.Add(newCartDetail);
                }

                // Update or add the cart to the database
                if (cart != null)
                {
                    if (cart.Id == Guid.Empty)
                    {
                        // If it's a new cart or doesn't exist in the database, add it
                        _ = context.Carts.Add(cart);
                    }
                    else
                    {
                        // If the cart already exists, update it in the database
                        _ = context.Carts.Update(cart);
                    }
                }

                // Save changes to the database
                _ = await context.SaveChangesAsync();


                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions and log errors
                Console.WriteLine($"Error in AddProductToCart: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// GetProductPriceAsync
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private async Task<string> GetProductPriceAsync(Guid productId)
        {
            try
            {
                // Use the injected ProductService to get the product price
                string productPrice = await _productService.GetProductPriceById(productId);
                return productPrice;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors
                Console.WriteLine($"Error while getting product price by ID {productId}: {ex.Message}");
                return string.Empty; // Return an empty string or handle the error case accordingly
            }
        }

        /// <summary>
        /// ViewCart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CartResponse> ViewCart(Guid userId)
        {
            return await GetCartByUserId(userId);
        }

        /// <summary>
        /// DeleteProductFromCart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductFromCart(Guid userId, Guid productId)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                Cart? cart = await context.Carts
                    .Include(c => c.CartDetails)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart != null && cart.CartDetails != null)
                {
                    CartDetail? cartItem = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == productId);

                    if (cartItem != null)
                    {
                        _ = context.CartDetails?.Remove(cartItem);
                        _ = await context.SaveChangesAsync();
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                // Handle exceptions or log errors
                return false;
            }
        }

        /// <summary>
        /// UpdateQuantity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateQuantity(Guid userId, Guid productId, int quantity)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                Cart? cart = await context.Carts
                    .Include(c => c.CartDetails)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart != null)
                {
                    CartDetail? cartItem = cart.CartDetails?.FirstOrDefault(cd => cd.ProductId == productId);

                    if (cartItem != null)
                    {
                        cartItem.Quantity = quantity;
                        _ = await context.SaveChangesAsync();
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                // Handle exceptions or log errors
                return false;
            }
        }
    }
}
