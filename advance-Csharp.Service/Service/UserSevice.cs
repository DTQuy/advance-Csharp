using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// UserGetListResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserGetListResponse> GetApplicationUserList(UserGetListRequest request)
        {
            UserGetListResponse userGetListResponse = new()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };
            using (AdvanceCsharpContext context = new())
            {
                IQueryable<User> query = context.Users ?? Enumerable.Empty<User>().AsQueryable();
                if (query == null)
                {
                    return userGetListResponse;
                }
                if (!string.IsNullOrEmpty(request.Email)) 
                {
                   query = query.Where(a => a.Email.Contains(request.Email));
                }
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    query = query.Where(a => a.PhoneNumber.Contains(request.PhoneNumber));
                }

                // Count the total number of User according to filtered conditions
                userGetListResponse.TotalUser = await query.CountAsync();

                // Calculate the number of pages and total pages
                int totalPages = (int)Math.Ceiling((double)userGetListResponse.TotalUser / request.PageSize);
                userGetListResponse.TotalPages = totalPages;

                // Perform pagination and get data for the current page
                int startIndex = (request.PageIndex - 1) * request.PageSize;
                int endIndex = startIndex + request.PageSize;
                query = query.Skip(startIndex).Take(request.PageSize);

                userGetListResponse.Data = await query.Select(a => new UserResponse
                {
                    Id = a.Id,
                    LastName = a.LastName,
                    FirstName = a.FirstName,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    Address = a.Address,
                    CreatedAt = a.CreatedAt,
                }).ToListAsync();
            }
            return userGetListResponse;

        }

        /// <summary>
        /// create-User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserCreateResponse> CreateUser(UserCreateRequest request)
        {
            try
            {
                User newUser = new()
                {
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address
                };

                using (AdvanceCsharpContext context = new())
                {
                    if (context.Users != null)
                    {
                        _ = context.Users.Add(newUser);
                        _ = await context.SaveChangesAsync();
                    }
                }

                // create DTO to user info
                UserResponse userResponse = new()
                {
                    Id = newUser.Id,
                    LastName = newUser.LastName,
                    FirstName = newUser.FirstName,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    PhoneNumber = newUser.PhoneNumber,
                    Address = newUser.Address
                };

                // create DTO to respons
                UserCreateResponse response = new()
                {
                    Message = "User created successfully",
                    userResponse = userResponse
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
        /// Update User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserUpdateResponse> UpdateUser(UserUpdateRequest request)
        {
            try
            {
                // Check if the request is valid
                if (!request.IsPhoneValid)
                {
                    return new UserUpdateResponse
                    {
                        Message = "Invalid Phone number format. Please enter a valid number."
                    };
                }

                using AdvanceCsharpContext context = new();
                // Check if context.User is null
                if (context.Users == null)
                {
                    // Handle the case where context.User is null
                    return new UserUpdateResponse
                    {
                        Message = "Error: context.User is null."
                    };
                }

                var existingUser = await context.Users.FindAsync(request.Id);

                if (existingUser == null)
                {
                    return new UserUpdateResponse
                    {
                        Message = "Product not found"
                    };
                }

                // Save old User information
                UserResponse oldUser = new()
                {
                    Id = existingUser.Id,
                    LastName = existingUser.LastName,
                    FirstName = existingUser.FirstName,
                    Email = existingUser.Email,
                    Password = existingUser.Password,
                    PhoneNumber = existingUser.PhoneNumber,
                    Address = existingUser.Address,
                    CreatedAt = existingUser.CreatedAt
                };

                // Update User information
                existingUser.LastName = request.LastName;
                existingUser.FirstName = request.FirstName;
                existingUser.Email = request.Email;
                if (!string.IsNullOrEmpty(request.Password))
                {
                    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }
                existingUser.PhoneNumber = request.PhoneNumber;
                existingUser.Address = request.Address;

                // Save changes to the database
                _ = await context.SaveChangesAsync();

                // Generate DTO for product information after update
                UserResponse updatedUser = new()
                {
                    Id = existingUser.Id,
                    LastName = existingUser.LastName,
                    FirstName = existingUser.FirstName,
                    Email = existingUser.Email,
                    Password = existingUser.Password,
                    PhoneNumber = existingUser.PhoneNumber,
                    Address = existingUser.Address,
                    CreatedAt = existingUser.CreatedAt
                };

                // Create DTO for response
                UserUpdateResponse response = new()
                {
                    Message = "Product updated successfully",
                    OldUser = oldUser,
                    UpdatedUser = updatedUser
                };

                return response;
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return new UserUpdateResponse
                {
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDeleteResponse> DeleteUser(Guid id)
        {
            try
            {
                using AdvanceCsharpContext context = new();
                // Check if context.User is null
                if (context.Users == null)
                {
                    // Handle the case where context.Products is null
                    return new UserDeleteResponse("Error: context.Products is null", new UserResponse());
                }
                // Check if the product exists
                User? existingUser = await context.Users.FindAsync(id);

                if (existingUser == null)
                {
                    return new UserDeleteResponse("Product not found", new UserResponse());
                }

                // Save old user information
                UserResponse deletedUser = new()
                {
                    Id = existingUser.Id,
                    LastName = existingUser.LastName ?? string.Empty,
                    FirstName = existingUser.FirstName ?? string.Empty,
                    Email = existingUser.Email,
                    Password = existingUser.Password ?? string.Empty,
                    PhoneNumber = existingUser.PhoneNumber ?? string.Empty,
                    Address = existingUser.Address ?? string.Empty
                };

                // Delete user
                _ = context.Users.Remove(existingUser);

                // Save changes to the database
                _ = await context.SaveChangesAsync();

                // Returns a success message and information about the deleted user
                return new UserDeleteResponse("Product deleted successfully", deletedUser);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return new UserDeleteResponse("Error deleting product", new UserResponse());
            }
        }

        public Task<UserDeleteResponse> DeleteUser(UserDeleteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
