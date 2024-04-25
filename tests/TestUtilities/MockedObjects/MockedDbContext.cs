using DataAccessLayer;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;
using TestUtilities.FakeSeeding;

namespace TestUtilities.MockedObjects;

public static class MockedDbContext
{
    public static string RandomDbName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDbContextOptions()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
            .UseInMemoryDatabase(RandomDbName)
            .Options;

        return dbContextOptions;
    }

    public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
    {
        var dbContextToMock = new BookHubDbContext(options);

        var dbContext = new MockedDbContextBuilder<BookHubDbContext>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options)
            .MockedDbContext;

        PrepareData(dbContext);

        return dbContext;
    }

    public static void PrepareData(BookHubDbContext dbContext)
    {
        FakeDataInitializer.Seed(dbContext);

        dbContext.SaveChanges();
    }

    public static async Task PrepareDataAsync(BookHubDbContext dbContext)
    {
        FakeDataInitializer.Seed(dbContext);

        await dbContext.SaveChangesAsync();
    }
}
