using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class PublisherSeeder
{
    internal static List<Publisher> PreparePublisherModels()
    {
        return new List<Publisher>
        {
            new() { Id = 1, Name = "HarperCollins" },
            new() { Id = 2, Name = "Penguin Random House" },
            new() { Id = 3, Name = "Simon & Schuster" },
            new() { Id = 4, Name = "Hachette Livre" },
            new() { Id = 5, Name = "Macmillan Publishers" },
            new() { Id = 6, Name = "Scholastic Corporation" },
            new() { Id = 7, Name = "Pearson PLC" },
            new() { Id = 8, Name = "Oxford University Press" }
        };
    }
}
