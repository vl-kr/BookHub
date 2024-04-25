using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.DTOs.Responses.ShoppingCartItem;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IShoppingCartItemService
{
    Task<ServiceResult<ShoppingCartItemResponse>> CreateShoppingCartItem(
        ShoppingCartItemRequest shoppingCartItemRequest
    );

    Task<ServiceResult<IEnumerable<ShoppingCartItemResponse>>> GetShoppingCartItems(
        PageOptions pageOptions
    );

    Task<ServiceResult<ShoppingCartItemResponse>> GetShoppingCartItem(int id);

    Task<ServiceResult<ShoppingCartItemResponse>> UpdateShoppingCartItem(
        int id,
        ShoppingCartItemRequest shoppingCartItemRequest
    );

    Task<ServiceResult<ShoppingCartItemResponse>> DeleteShoppingCartItem(int id);
    Task<ServiceResult<ShoppingCartItemResponse?>> GetCartItemOfBook(int id, int bookId);

    Task<ServiceResult<ShoppingCartItemResponse>> ChangeQuantity(int id, int newQuantity);
}
