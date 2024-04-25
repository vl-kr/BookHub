using DataAccessLayer;

namespace TestUtilities.FakeSeeding;

public static class FakeDataInitializer
{
    public static void Seed(BookHubDbContext dbContext)
    {
        var authors = AuthorSeeder.PrepareAuthorModels();
        var publishers = PublisherSeeder.PreparePublisherModels();
        var books = BookSeeder.PrepareBookModels();
        var reviews = ReviewSeeder.PrepareReviewModels();
        var customers = CustomerSeeder.PrepareCustomerModels();
        var genres = GenreSeeder.PrepareGenreModels();
        var orders = OrderSeeder.PrepareOrderModels();
        var orderItems = OrderItemSeeder.PrepareOrderItemModels();
        var wishlists = WishlistSeeder.PrepareWishlistModels();
        var wishlistItems = WishlistItemSeeder.PrepareWishlistItemModels();
        var orderStatuses = OrderStatusSeeder.PrepareOrderStatusModels();
        var paymentMethods = PaymentMethodSeeder.PreparePaymentMethodModels();
        var shippingMethods = ShippingMethodSeeder.PrepareShippingMethodModels();
        var addresses = AddressSeeder.PrepareAddressModels();
        var shoppingCarts = ShoppingCartSeeder.PrepareShoppingCartModels();
        var shoppingCartItems = ShoppingCartItemSeeder.PrepareShoppingCartItemModels();

        dbContext.Authors.AddRange(authors);
        dbContext.Books.AddRange(books);
        dbContext.Reviews.AddRange(reviews);
        dbContext.Customers.AddRange(customers);
        dbContext.Genres.AddRange(genres);
        dbContext.Orders.AddRange(orders);
        dbContext.OrderItems.AddRange(orderItems);
        dbContext.Publishers.AddRange(publishers);
        dbContext.Wishlists.AddRange(wishlists);
        dbContext.WishlistItems.AddRange(wishlistItems);
        dbContext.OrderStatuses.AddRange(orderStatuses);
        dbContext.Addresses.AddRange(addresses);
        dbContext.PaymentMethods.AddRange(paymentMethods);
        dbContext.ShippingMethods.AddRange(shippingMethods);
        dbContext.ShoppingCarts.AddRange(shoppingCarts);
        dbContext.ShoppingCartItems.AddRange(shoppingCartItems);
    }
}
