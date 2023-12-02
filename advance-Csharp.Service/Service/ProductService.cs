﻿using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// ProductGetListResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductGetListResponse> GetApplicationProductList(ProductGetListRequest request)
        {
            ProductGetListResponse productGetListResponse = new()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };

            using (AdvanceCsharpContext context = new())
            {
                IQueryable<Product> query = context.Products;

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(a => a.Name.Contains(request.Name));
                }

                if (!string.IsNullOrEmpty(request.Category))
                {
                    query = query.Where(a => a.Category.Contains(request.Category));
                }

                if (!string.IsNullOrEmpty(request.PriceFrom))
                {
                    decimal priceFrom = Convert.ToDecimal(request.PriceFrom);
                    query = query.Where(a => Convert.ToDecimal(a.Price) >= priceFrom);
                }

                if (!string.IsNullOrEmpty(request.PriceTo))
                {
                    decimal priceTo = Convert.ToDecimal(request.PriceTo);
                    query = query.Where(a => Convert.ToDecimal(a.Price) <= priceTo);
                }
                productGetListResponse.Total = await query.CountAsync();

                productGetListResponse.Data = await query.Select(a => new ProductResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Category = a.Category,
                    Price = a.Price,
                }).ToListAsync();
            }

            return productGetListResponse;
        }

        /// <summary>
        /// create-product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductCreateResponse> CreateProduct(ProductCreateRequest request)
        {
            try
            {
                Product newProduct = new()
                {
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    Unit = request.Unit,
                    Images = request.Images,
                    Category = request.Category
                };

                using (AdvanceCsharpContext context = new())
                {
                    // add data to SQL
                    _ = context.Products.Add(newProduct);
                    _ = await context.SaveChangesAsync();
                }

                // create DTO to product info
                ProductResponse productResponse = new()
                {
                    Id = newProduct.Id,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Quantity = newProduct.Quantity,
                    Unit = newProduct.Unit,
                    Images = newProduct.Images,
                    Category = newProduct.Category
                };

                // create DTO to respons
                ProductCreateResponse response = new()
                {
                    Message = "Product created successfully",
                    productResponse = productResponse
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductUpdateResponse> UpdateProduct(ProductUpdateRequest request)
        {
            try
            {
                // Check if the request is valid
                if (!request.IsPriceValid)
                {
                    return new ProductUpdateResponse
                    {
                        Message = "Invalid Price format. Please enter a valid number."
                    };
                }

                using AdvanceCsharpContext context = new();
                // Check if the product exists
                Product existingProduct = await context.Products.FindAsync(request.Id);
                if (existingProduct == null)
                {
                    return new ProductUpdateResponse
                    {
                        Message = "Product not found"
                    };
                }

                // Save old product information
                ProductResponse oldProduct = new()
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Price = existingProduct.Price,
                    Quantity = existingProduct.Quantity,
                    Unit = existingProduct.Unit,
                    Images = existingProduct.Images,
                    Category = existingProduct.Category,
                    CreatedAt = existingProduct.CreatedAt
                };

                // Update product information
                existingProduct.Name = request.Name;
                existingProduct.Price = request.Price;
                existingProduct.Quantity = request.Quantity;
                existingProduct.Unit = request.Unit;
                existingProduct.Images = request.Images;
                existingProduct.Category = request.Category;

                // Save changes to the database
                _ = await context.SaveChangesAsync();

                // Generate DTO for product information after update
                ProductResponse updatedProduct = new()
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Price = existingProduct.Price,
                    Quantity = existingProduct.Quantity,
                    Unit = existingProduct.Unit,
                    Images = existingProduct.Images,
                    Category = existingProduct.Category,
                    CreatedAt = existingProduct.CreatedAt
                };

                // Create DTO for response
                ProductUpdateResponse response = new()
                {
                    Message = "Product updated successfully",
                    OldProduct = oldProduct,
                    UpdatedProduct = updatedProduct
                };

                return response;
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return new ProductUpdateResponse
                {
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDeleteResponse> DeleteProduct(Guid id)
        {
            try
            {
                using AdvanceCsharpContext context = new();
                // Check if the product exists
                Product existingProduct = await context.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    return new ProductDeleteResponse("Product not found", null);
                }

                // Save old product information
                ProductResponse deletedProduct = new()
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name ?? string.Empty,
                    Price = existingProduct.Price ?? string.Empty,
                    Quantity = existingProduct.Quantity,
                    Unit = existingProduct.Unit ?? string.Empty,
                    Images = existingProduct.Images ?? string.Empty,
                    Category = existingProduct.Category ?? string.Empty
                };

                // Delete product
                _ = context.Products.Remove(existingProduct);

                // Save changes to the database
                _ = await context.SaveChangesAsync();

                // Returns a success message and information about the deleted product
                return new ProductDeleteResponse("Product deleted successfully", deletedProduct);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return new ProductDeleteResponse("Error deleting product", null);
            }
        }


        public Task<ProductDeleteResponse> DeleteProduct(ProductDeleteRequest request)
        {
            throw new NotImplementedException();
        }
    }

}