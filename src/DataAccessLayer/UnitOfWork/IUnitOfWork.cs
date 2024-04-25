using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IBookRepository BookRepository { get; }
    IGenreRepository GenreRepository { get; }
    IPublisherRepository PublisherRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IAddressRepository AddressRepository { get; }
    IOrderStatusRepository OrderStatusRepository { get; }
    IPaymentMethodRepository PaymentMethodRepository { get; }
    IShippingMethodRepository ShippingMethodRepository { get; }
    IWishlistRepository WishlistRepository { get; }
    IWishlistItemRepository WishlistItemRepository { get; }
    IShoppingCartRepository ShoppingCartRepository { get; }
    IShoppingCartItemRepository ShoppingCartItemRepository { get; }
    ILocalIdentityUserRepository LocalIdentityUserRepository { get; }

    Task CommitAsync();
    IDbContextTransaction BeginTransaction();
}
