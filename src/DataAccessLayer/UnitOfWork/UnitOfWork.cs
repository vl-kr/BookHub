using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookHubDbContext _context;
    private readonly UserManager<LocalIdentityUser> _userManager;
    private AddressRepository? _addressRepository;
    private AuthorRepository? _authorRepository;

    private BookRepository? _bookRepository;
    private CustomerRepository? _customerRepository;
    private GenreRepository? _genreRepository;
    private LocalIdentityUserRepository? _localIdentityUserRepository;
    private OrderItemRepository? _orderItemRepository;
    private OrderRepository? _orderRepository;
    private OrderStatusRepository? _orderStatusRepository;
    private PaymentMethodRepository? _paymentMethodRepository;
    private PublisherRepository? _publisherRepository;
    private ReviewRepository? _reviewRepository;
    private ShippingMethodRepository? _shippingMethodRepository;
    private ShoppingCartItemRepository? _shoppingCartItemRepository;
    private ShoppingCartRepository? _shoppingCartRepository;
    private WishlistItemRepository? _wishlistItemRepository;
    private WishlistRepository? _wishlistRepository;

    public UnitOfWork(BookHubDbContext dbContext, UserManager<LocalIdentityUser> userManager)
    {
        _context = dbContext;
        _userManager = userManager;
    }

    public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);

    public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);

    public IPublisherRepository PublisherRepository =>
        _publisherRepository ??= new PublisherRepository(_context);

    public IAuthorRepository AuthorRepository =>
        _authorRepository ??= new AuthorRepository(_context);

    public IReviewRepository ReviewRepository =>
        _reviewRepository ??= new ReviewRepository(_context);

    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);

    public IOrderItemRepository OrderItemRepository =>
        _orderItemRepository ??= new OrderItemRepository(_context);

    public ICustomerRepository CustomerRepository =>
        _customerRepository ??= new CustomerRepository(_context);

    public IAddressRepository AddressRepository =>
        _addressRepository ??= new AddressRepository(_context);

    public IOrderStatusRepository OrderStatusRepository =>
        _orderStatusRepository ??= new OrderStatusRepository(_context);

    public IPaymentMethodRepository PaymentMethodRepository =>
        _paymentMethodRepository ??= new PaymentMethodRepository(_context);

    public IShippingMethodRepository ShippingMethodRepository =>
        _shippingMethodRepository ??= new ShippingMethodRepository(_context);

    public IWishlistRepository WishlistRepository =>
        _wishlistRepository ??= new WishlistRepository(_context);

    public IWishlistItemRepository WishlistItemRepository =>
        _wishlistItemRepository ??= new WishlistItemRepository(_context);

    public IShoppingCartRepository ShoppingCartRepository =>
        _shoppingCartRepository ??= new ShoppingCartRepository(_context);

    public IShoppingCartItemRepository ShoppingCartItemRepository =>
        _shoppingCartItemRepository ??= new ShoppingCartItemRepository(_context);

    public ILocalIdentityUserRepository LocalIdentityUserRepository =>
        _localIdentityUserRepository ??= new LocalIdentityUserRepository(_context, _userManager);

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }
}
