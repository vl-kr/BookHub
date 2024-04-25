using BusinessLayer;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestUtilities.MockedObjects;

public class MockedDependencyInjectionBuilder
{
    protected IServiceCollection ServiceCollection = new ServiceCollection();

    public MockedDependencyInjectionBuilder AddMockedDbContext()
    {
        ServiceCollection = ServiceCollection.AddDbContext<BookHubDbContext>(options =>
            options.UseInMemoryDatabase(MockedDbContext.RandomDbName)
        );
        return this;
    }

    public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
        where T : class
    {
        ServiceCollection = ServiceCollection.AddScoped(_ => objectToRegister);

        return this;
    }

    public MockedDependencyInjectionBuilder AddRepositories()
    {
        ServiceCollection = ServiceCollection
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IAddressRepository, AddressRepository>()
            .AddScoped<IPublisherRepository, PublisherRepository>()
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IOrderItemRepository, OrderItemRepository>()
            .AddScoped<IOrderStatusRepository, OrderStatusRepository>()
            .AddScoped<IPaymentMethodRepository, PaymentMethodRepository>()
            .AddScoped<IShippingMethodRepository, ShippingMethodRepository>()
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<IWishlistRepository, WishlistRepository>()
            .AddScoped<IWishlistItemRepository, WishlistItemRepository>()
            .AddScoped<IShoppingCartRepository, ShoppingCartRepository>()
            .AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();

        return this;
    }

    public MockedDependencyInjectionBuilder AddUnitOfWork()
    {
        ServiceCollection = ServiceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        return this;
    }

    public MockedDependencyInjectionBuilder AddAutoMapper()
    {
        ServiceCollection = ServiceCollection.AddAutoMapper(typeof(MappingProfile));

        return this;
    }

    public MockedDependencyInjectionBuilder AddIdentity()
    {
        ServiceCollection
            .AddDefaultIdentity<LocalIdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<BookHubDbContext>();

        return this;
    }

    public ServiceProvider Create()
    {
        return ServiceCollection.BuildServiceProvider();
    }

    public MockedDependencyInjectionBuilder AddServices()
    {
        ServiceCollection = ServiceCollection
            .AddScoped<IBookService, BookService>()
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IAddressService, AddressService>()
            .AddScoped<IPublisherService, PublisherService>()
            .AddScoped<IGenreService, GenreService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IOrderItemService, OrderItemService>()
            .AddScoped<IOrderStatusService, OrderStatusService>()
            .AddScoped<IPaymentMethodService, PaymentMethodService>()
            .AddScoped<IShippingMethodService, ShippingMethodService>()
            .AddScoped<IWishlistService, WishlistService>()
            .AddScoped<IWishlistItemService, WishlistItemService>()
            .AddScoped<IShoppingCartService, ShoppingCartService>()
            .AddScoped<IShoppingCartItemService, ShoppingCartItemService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ILocalIdentityUserService, LocalIdentityUserService>();

        return this;
    }
}
