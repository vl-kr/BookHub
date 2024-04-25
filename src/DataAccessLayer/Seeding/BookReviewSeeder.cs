using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class ReviewSeeder
{
    internal static List<Review> PrepareReviewModels()
    {
        return new List<Review>
        {
            new()
            {
                Id = 1,
                Rating = 5,
                TextReview = "This book is amazing!",
                BookId = 1,
                CustomerId = 3
            },
            new()
            {
                Id = 2,
                Rating = 4,
                TextReview = "This book is pretty good!",
                BookId = 2,
                CustomerId = 4
            },
            new()
            {
                Id = 3,
                Rating = 3,
                TextReview = "This book is ok!",
                BookId = 2,
                CustomerId = 3
            },
            new()
            {
                Id = 4,
                Rating = 2,
                TextReview = "This book is pretty bad!",
                BookId = 4,
                CustomerId = 3
            },
            new()
            {
                Id = 5,
                Rating = 1,
                TextReview = "This book is terrible!",
                BookId = 2,
                CustomerId = 7
            },
            new()
            {
                Id = 6,
                Rating = 4,
                TextReview = "An enchanting read that captivated my imagination!",
                BookId = 18,
                CustomerId = 9
            },
            new()
            {
                Id = 7,
                Rating = 3,
                TextReview = "Decent book, but not my favorite.",
                BookId = 19,
                CustomerId = 10
            },
            new()
            {
                Id = 8,
                Rating = 5,
                TextReview = "A gripping page-turner that held my attention till the end!",
                BookId = 12,
                CustomerId = 5
            },
            new()
            {
                Id = 9,
                Rating = 2,
                TextReview = "Disappointing book, didn't live up to the hype.",
                BookId = 20,
                CustomerId = 10
            },
            new()
            {
                Id = 10,
                Rating = 4,
                TextReview = "An enjoyable and thought-provoking literary journey.",
                BookId = 13,
                CustomerId = 3
            },
            new()
            {
                Id = 11,
                Rating = 5,
                TextReview = "This book is a masterpiece that will stay with me forever!",
                BookId = 16,
                CustomerId = 6
            },
            new()
            {
                Id = 12,
                Rating = 4,
                TextReview = "A classic that never gets old, a must-read!",
                BookId = 17,
                CustomerId = 7
            },
            new()
            {
                Id = 13,
                Rating = 3,
                TextReview = "It was an interesting story, but it didn't fully resonate with me.",
                BookId = 14,
                CustomerId = 8
            },
            new()
            {
                Id = 14,
                Rating = 5,
                TextReview = "A compelling narrative with well-developed characters!",
                BookId = 10,
                CustomerId = 1
            },
            new()
            {
                Id = 15,
                Rating = 4,
                TextReview = "This book made me ponder the meaning of life.",
                BookId = 15,
                CustomerId = 4
            },
            new()
            {
                Id = 16,
                Rating = 4,
                TextReview = "A thought-provoking and engaging story that kept me hooked!",
                BookId = 19,
                CustomerId = 9
            },
            new()
            {
                Id = 17,
                Rating = 3,
                TextReview = "Decent read, but not a personal favorite.",
                BookId = 18,
                CustomerId = 6
            },
            new()
            {
                Id = 18,
                Rating = 5,
                TextReview = "This book is a literary gem, a must-read!",
                BookId = 17,
                CustomerId = 3
            },
            new()
            {
                Id = 19,
                Rating = 2,
                TextReview = "A disappointment, didn't live up to expectations.",
                BookId = 20,
                CustomerId = 8
            },
            new()
            {
                Id = 20,
                Rating = 4,
                TextReview = "An unforgettable narrative with well-developed characters.",
                BookId = 12,
                CustomerId = 1
            },
            new()
            {
                Id = 21,
                Rating = 5,
                TextReview = "This book is a true masterpiece of literature!",
                BookId = 16,
                CustomerId = 4
            },
            new()
            {
                Id = 22,
                Rating = 4,
                TextReview = "A classic that continues to captivate readers.",
                BookId = 13,
                CustomerId = 10
            },
            new()
            {
                Id = 23,
                Rating = 3,
                TextReview = "An interesting story, but not my top choice.",
                BookId = 15,
                CustomerId = 5
            },
            new()
            {
                Id = 24,
                Rating = 5,
                TextReview = "This book was a delightful journey with rich characters.",
                BookId = 14,
                CustomerId = 7
            },
            new()
            {
                Id = 25,
                Rating = 4,
                TextReview = "A philosophical exploration that left me thinking.",
                BookId = 11,
                CustomerId = 2
            }
        };
    }
}
