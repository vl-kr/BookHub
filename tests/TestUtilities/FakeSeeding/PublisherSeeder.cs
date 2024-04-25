using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class PublisherSeeder
{
    public static List<Publisher> PreparePublisherModels()
    {
        return new List<Publisher>
        {
            new Publisher { Id = 1, Name = "HarperCollins" },
            new Publisher { Id = 2, Name = "Penguin Random House" },
            new Publisher { Id = 3, Name = "Simon & Schuster" },
            new Publisher { Id = 4, Name = "Hachette Livre" },
            new Publisher { Id = 5, Name = "Macmillan Publishers" },
            new Publisher { Id = 6, Name = "Scholastic Corporation" },
            new Publisher { Id = 7, Name = "Pearson PLC" },
            new Publisher { Id = 8, Name = "Oxford University Press" }
        };
    }
}
