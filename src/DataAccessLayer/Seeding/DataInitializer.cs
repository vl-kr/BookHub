using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Seeding;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var users = LocalIdentityUserSeeder.PrepareUserModels();
        var roles = IdentityRoleSeeder.PrepareRoleModels();
        var userRoles = IdentityUserRoleSeeder.PrepareUserRoleModels();

        var authors = AuthorSeeder.PrepareAuthorModels();
        var publishers = PublisherSeeder.PreparePublisherModels();
        var books = BookSeeder.PrepareBookModels();
        var reviews = ReviewSeeder.PrepareReviewModels();
        var customers = CustomerSeeder.PrepareCustomerModels();
        var genres = GenreSeeder.PrepareGenreModels();
        var bookGenres = BookGenreSeeder.PrepareBookGenreModels();
        var bookAuthors = BookAuthorSeeder.PrepareBookAuthorModels();
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

        modelBuilder.Entity<LocalIdentityUser>().HasData(users);
        modelBuilder.Entity<IdentityRole<int>>().HasData(roles);
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(userRoles);

        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Book>().HasData(books);
        modelBuilder.Entity<Review>().HasData(reviews);
        modelBuilder.Entity<Customer>().HasData(customers);
        modelBuilder.Entity<Genre>().HasData(genres);
        modelBuilder.Entity<BookGenre>().HasData(bookGenres);
        modelBuilder.Entity<BookAuthor>().HasData(bookAuthors);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<OrderItem>().HasData(orderItems);
        modelBuilder.Entity<Publisher>().HasData(publishers);
        modelBuilder.Entity<Wishlist>().HasData(wishlists);
        modelBuilder.Entity<WishlistItem>().HasData(wishlistItems);
        modelBuilder.Entity<OrderStatus>().HasData(orderStatuses);
        modelBuilder.Entity<Address>().HasData(addresses);
        modelBuilder.Entity<PaymentMethod>().HasData(paymentMethods);
        modelBuilder.Entity<ShippingMethod>().HasData(shippingMethods);
        modelBuilder.Entity<ShoppingCart>().HasData(shoppingCarts);
        modelBuilder.Entity<ShoppingCartItem>().HasData(shoppingCartItems);
    }
}
