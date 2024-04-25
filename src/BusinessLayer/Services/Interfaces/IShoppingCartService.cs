using BusinessLayer.DTOs.Requests.ShoppingCart;
using BusinessLayer.DTOs.Responses.ShoppingCart;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IShoppingCartService
{
    Task<ServiceResult<ShoppingCartResponse>> CreateShoppingCart(
        ShoppingCartRequest shoppingCartRequest
    );

    Task<ServiceResult<IEnumerable<ShoppingCartResponse>>> GetShoppingCarts(
        PageOptions pageOptions
    );

    Task<ServiceResult<ShoppingCartResponse>> GetShoppingCart(int id);

    Task<ServiceResult<ShoppingCartResponse>> UpdateShoppingCart(
        int id,
        ShoppingCartRequest shoppingCartRequest
    );

    Task<ServiceResult<ShoppingCartResponse>> DeleteShoppingCart(int id);

    Task<ShoppingCart?> GetShoppingCartOfCustomer(int customerId);
}
