using BusinessLayer.Coordinators;
using BusinessLayer.Coordinators.Interfaces;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<IWishlistItemService, WishlistItemService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<IShippingMethodService, ShippingMethodService>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ILocalIdentityUserService, LocalIdentityUserService>();
    }

    public static void RegisterCoordinators(this IServiceCollection services)
    {
        services.AddScoped<ICartToOrderCoordinator, CartToOrderCoordinator>();
        services.AddScoped<IWishlistToCartCoordinator, WishlistToCartCoordinator>();
        services.AddScoped<ISearchCoordinator, SearchCoordinator>();
    }
}
