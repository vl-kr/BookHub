using DataAccessLayer.Entities;
using DataAccessLayer.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class BookHubDbContext : IdentityDbContext<LocalIdentityUser, IdentityRole<int>, int>
{
    public BookHubDbContext(DbContextOptions<BookHubDbContext> options)
        : base(options) { }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<WishlistItem> WishlistItems { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Wishlist> Wishlists { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(a => a.Books)
            .UsingEntity<BookGenre>(
                j => j.HasOne(bg => bg.Genre).WithMany().HasForeignKey(bg => bg.GenreId),
                j => j.HasOne(bg => bg.Book).WithMany().HasForeignKey(bg => bg.BookId)
            );

        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<BookAuthor>(
                j => j.HasOne(ba => ba.Author).WithMany().HasForeignKey(ba => ba.AuthorId),
                j => j.HasOne(ba => ba.Book).WithMany().HasForeignKey(ba => ba.BookId)
            );

        modelBuilder
            .Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())
            .ToList()
            .ForEach(foreignKey => foreignKey.DeleteBehavior = DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>().Property(b => b.Price).HasColumnType("decimal(18, 2)");

        modelBuilder
            .Entity<OrderItem>()
            .Property(o => o.TotalPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.PrimaryGenre)
            .WithMany()
            .HasForeignKey(b => b.PrimaryGenreId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(p => p.Book)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(o => o.OrderStatusId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.PaymentMethod)
            .WithMany()
            .HasForeignKey(o => o.PaymentMethodId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.ShippingMethod)
            .WithMany()
            .HasForeignKey(o => o.ShippingMethodId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.ShippingAddress)
            .WithMany()
            .HasForeignKey(o => o.ShippingAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.BillingAddress)
            .WithMany()
            .HasForeignKey(o => o.BillingAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Order>()
            .HasMany(oi => oi.OrderItems)
            .WithOne(o => o.Order)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Book)
            .WithMany()
            .HasForeignKey(oi => oi.BookId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<LocalIdentityUser>()
            .HasOne(u => u.Customer)
            .WithOne(c => c.User)
            .HasForeignKey<Customer>(c => c.UserId);

        modelBuilder
            .Entity<ShoppingCart>()
            .HasMany(c => c.ShoppingCartItems)
            .WithOne(i => i.ShoppingCart)
            .HasForeignKey(i => i.ShoppingCartId);

        modelBuilder
            .Entity<ShoppingCartItem>()
            .HasOne(i => i.Book)
            .WithMany()
            .HasForeignKey(i => i.BookId);

        modelBuilder
            .Entity<Wishlist>()
            .HasMany(c => c.WishlistItems)
            .WithOne(i => i.Wishlist)
            .HasForeignKey(i => i.WishlistId);

        modelBuilder
            .Entity<WishlistItem>()
            .HasOne(i => i.Book)
            .WithMany()
            .HasForeignKey(i => i.BookId);

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
