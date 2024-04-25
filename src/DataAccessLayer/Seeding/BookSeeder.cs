using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class BookSeeder
{
    internal static List<Book> PrepareBookModels()
    {
        return new List<Book>
        {
            new()
            {
                Id = 1,
                Title = "The Hobbit",
                Description =
                    "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction. The book remains popular and is recognized as a classic in children's literature.",
                Price = 24.99m,
                ISBN = "9780006754024",
                YearPublished = 1937,
                ImageUrl = "https://m.media-amazon.com/images/I/710+HcoP38L._SY466_.jpg",
                PublisherId = 1,
                PrimaryGenreId = 1
            },
            new()
            {
                Id = 2,
                Title = "A Game of Thrones",
                Description =
                    "A Game of Thrones is the first novel in A Song of Ice and Fire, a series of fantasy novels by the American author George R. R. Martin. It was first published on August 1, 1996. The novel won the 1997 Locus Award and was nominated for both the 1997 Nebula Award and the 1997 World Fantasy Award.",
                Price = 9.99m,
                ISBN = "9780553103540",
                YearPublished = 1996,
                ImageUrl = "https://m.media-amazon.com/images/I/81GdMqla0cL._SY466_.jpg",
                PublisherId = 2,
                PrimaryGenreId = 2
            },
            new()
            {
                Id = 3,
                Title = "The Shining",
                Description =
                    "The Shining is a horror novel by American author Stephen King. Published in 1977, it is King's third published novel and first hardback bestseller: the success of the book firmly established King as a preeminent author in the horror genre.",
                Price = 10.99m,
                ISBN = "9780385121675",
                YearPublished = 1977,
                ImageUrl = "https://m.media-amazon.com/images/I/81QckmGleYL._SY466_.jpg",
                PublisherId = 3,
                PrimaryGenreId = 3
            },
            new()
            {
                Id = 4,
                Title = "The Snowman",
                Description =
                    "The Snowman is a novel by Norwegian crime-writer Jo Nesbø. It is the seventh entry in his Harry Hole series. In Australia, the title was changed to Harry Hole: The Snowman, because of another 1991 novel of the same name by J.R. Rain.",
                Price = 9.99m,
                ISBN = "9780099520276",
                YearPublished = 2007,
                ImageUrl = "https://m.media-amazon.com/images/I/61uvYOfKHzL._SY466_.jpg",
                PublisherId = 4,
                PrimaryGenreId = 4
            },
            new()
            {
                Id = 5,
                Title = "The Return of the King",
                Description =
                    "The Return of the King is the third and final volume of J. R. R. Tolkien's The Lord of the Rings, following The Fellowship of the Ring and The Two Towers. The story begins in the kingdom of Gondor, which is soon to be attacked by the Dark Lord Sauron.",
                Price = 11.99m,
                ISBN = "9780618260553",
                YearPublished = 1955,
                ImageUrl = "https://m.media-amazon.com/images/I/91tZn9CjAwL._SY466_.jpg",
                PublisherId = 1,
                PrimaryGenreId = 5
            },
            new()
            {
                Id = 6,
                Title = "It",
                Description =
                    "It is a horror novel by American author Stephen King, published in 1986. It deals with themes that eventually became King staples: the power of memory, childhood trauma, and the ugliness lurking behind a façade of traditional small-town values.",
                Price = 14.99m,
                ISBN = "9780451169518",
                YearPublished = 1986,
                ImageUrl = "https://m.media-amazon.com/images/I/91jgm-KF0ZL._SL1500_.jpg",
                PublisherId = 3,
                PrimaryGenreId = 6
            },
            new()
            {
                Id = 7,
                Title = "The Catcher in the Rye",
                Description =
                    "The Catcher in the Rye is a novel by J.D. Salinger, published in 1951. It is a story of a young man named Holden Caulfield who embarks on a journey of self-discovery in New York City.",
                Price = 9.99m,
                ISBN = "9780316769488",
                YearPublished = 1951,
                ImageUrl = "https://m.media-amazon.com/images/I/71nXPGovoTL._SL1500_.jpg",
                PublisherId = 5,
                PrimaryGenreId = 7
            },
            new()
            {
                Id = 8,
                Title = "The Da Vinci Code",
                Description =
                    "The Da Vinci Code is a mystery thriller novel by Dan Brown, published in 2003. It follows the adventures of Robert Langdon, a Harvard professor of symbology, as he tries to solve a murder that leads to a series of secrets kept by a secret society.",
                Price = 10.99m,
                ISBN = "9780307474278",
                YearPublished = 2003,
                ImageUrl = "https://m.media-amazon.com/images/I/71QG6t0OOrL._SL1200_.jpg",
                PublisherId = 6,
                PrimaryGenreId = 8
            },
            new()
            {
                Id = 9,
                Title = "Pride and Prejudice",
                Description =
                    "Pride and Prejudice is a novel by Jane Austen, published in 1813. It is a classic of English literature and tells the story of Elizabeth Bennet and her romantic relationship with Mr. Darcy.",
                Price = 11.99m,
                ISBN = "9780486280575",
                YearPublished = 1813,
                ImageUrl = "https://m.media-amazon.com/images/I/81NLDvyAHrL._SL1500_.jpg",
                PublisherId = 8,
                PrimaryGenreId = 9
            },
            new()
            {
                Id = 10,
                Title = "The Great Gatsby",
                Description =
                    "The Great Gatsby is a novel by F. Scott Fitzgerald, published in 1925. It is a story of the American Dream, wealth, and love, set against the backdrop of the Roaring Twenties.",
                Price = 12.99m,
                ISBN = "9780743273565",
                YearPublished = 1925,
                ImageUrl = "https://m.media-amazon.com/images/I/61z0MrB6qOS._SL1500_.jpg",
                PublisherId = 7,
                PrimaryGenreId = 10
            },
            new()
            {
                Id = 11,
                Title = "The Girl with the Dragon Tattoo",
                Description =
                    "The Girl with the Dragon Tattoo is a crime thriller novel by Swedish author and journalist Stieg Larsson, published in 2005. It follows the investigation into the disappearance of a wealthy industrialist's niece and delves into family secrets.",
                Price = 16.99m,
                ISBN = "9780307269751",
                YearPublished = 2005,
                ImageUrl = "https://m.media-amazon.com/images/I/81UOA8fDGaL._SL1500_.jpg",
                PublisherId = 4,
                PrimaryGenreId = 11
            },
            new()
            {
                Id = 12,
                Title = "1984",
                Description =
                    "1984 is a dystopian novel by George Orwell, published in 1949. It portrays a totalitarian society where the government exercises control over every aspect of people's lives, including their thoughts and language.",
                Price = 11.99m,
                ISBN = "9780451524935",
                YearPublished = 1949,
                ImageUrl = "https://m.media-amazon.com/images/I/61u1QqUfL-L._SL1500_.jpg",
                PublisherId = 5,
                PrimaryGenreId = 12
            },
            new()
            {
                Id = 13,
                Title = "To Kill a Mockingbird",
                Description =
                    "To Kill a Mockingbird is a novel by Harper Lee, published in 1960. It addresses issues of racism, social injustice, and moral growth as seen through the eyes of a young girl in the American South.",
                Price = 8.99m,
                ISBN = "9780061120084",
                YearPublished = 1960,
                ImageUrl = "https://m.media-amazon.com/images/I/51tDHl8Z7cL.jpg",
                PublisherId = 1,
                PrimaryGenreId = 13
            },
            new()
            {
                Id = 14,
                Title = "The Alchemist",
                Description =
                    "The Alchemist is a novel by Brazilian author Paulo Coelho, originally published in Portuguese in 1988. It is a philosophical and inspirational book that tells the story of Santiago, a shepherd boy who embarks on a journey to fulfill his dreams.",
                Price = 5.99m,
                ISBN = "9780061122415",
                YearPublished = 1988,
                ImageUrl = "https://m.media-amazon.com/images/I/71zHDXu1TaL._SL1500_.jpg",
                PublisherId = 2,
                PrimaryGenreId = 1
            },
            new()
            {
                Id = 15,
                Title = "The Catcher in the Rye",
                Description =
                    "The Catcher in the Rye is a novel by J.D. Salinger, published in 1951. It is a story of a young man named Holden Caulfield who embarks on a journey of self-discovery in New York City.",
                Price = 7.99m,
                ISBN = "9780316769488",
                YearPublished = 1951,
                ImageUrl = "https://m.media-amazon.com/images/I/91iDBLW-vHL._SL1500_.jpg",
                PublisherId = 5,
                PrimaryGenreId = 2
            },
            new()
            {
                Id = 16,
                Title = "Moby-Dick",
                Description =
                    "Moby-Dick; or, The Whale is an epic novel by Herman Melville, published in 1851. It tells the story of Captain Ahab's obsessive quest to kill the giant white whale, Moby-Dick.",
                Price = 6.99m,
                ISBN = "9780143107319",
                YearPublished = 1851,
                ImageUrl = "https://m.media-amazon.com/images/I/71d5wo+-MuL._SL1200_.jpg",
                PublisherId = 1,
                PrimaryGenreId = 3
            },
            new()
            {
                Id = 17,
                Title = "The Road",
                Description =
                    "The Road is a post-apocalyptic novel by Cormac McCarthy, published in 2006. It follows a father and son's journey through a desolate, dangerous world in search of safety.",
                Price = 4.99m,
                ISBN = "9780307265432",
                YearPublished = 2006,
                ImageUrl = "https://m.media-amazon.com/images/I/51M7XGLQTBL._SL1200_.jpg",
                PublisherId = 1,
                PrimaryGenreId = 4
            },
            new()
            {
                Id = 18,
                Title = "The Hitchhiker's Guide to the Galaxy",
                Description =
                    "The Hitchhiker's Guide to the Galaxy is a comedic science fiction series by Douglas Adams, first published in 1979. It follows the misadventures of Arthur Dent, an unwitting Earthling who travels the universe with an alien friend.",
                Price = 15.99m,
                ISBN = "9780345391803",
                YearPublished = 1979,
                ImageUrl = "https://m.media-amazon.com/images/I/91pUhA4qZnL._SL1500_.jpg",
                PublisherId = 2,
                PrimaryGenreId = 5
            },
            new()
            {
                Id = 19,
                Title = "War and Peace",
                Description =
                    "War and Peace is an epic historical novel by Leo Tolstoy, published in 1869. It chronicles the events of Russian society during the Napoleonic era and features a large cast of characters.",
                Price = 10.99m,
                ISBN = "9780192833983",
                YearPublished = 1869,
                ImageUrl = "https://m.media-amazon.com/images/I/81bLfmgMcwL._SL1500_.jpg",
                PublisherId = 3,
                PrimaryGenreId = 6
            },
            new()
            {
                Id = 20,
                Title = "Brave New World",
                Description =
                    "Brave New World is a dystopian novel by Aldous Huxley, published in 1932. It explores a future society where advanced technology has eliminated suffering, but at the cost of personal freedom and individuality.",
                Price = 7.99m,
                ISBN = "9780060850524",
                YearPublished = 1932,
                ImageUrl = "https://m.media-amazon.com/images/I/71aDrgLp9CL._SL1360_.jpg",
                PublisherId = 6,
                PrimaryGenreId = 7
            }
        };
    }
}
