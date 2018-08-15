using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //int input = int.Parse(Console.ReadLine());
                // string command = Console.ReadLine();
                var result = RemoveBooks(db);
                 Console.WriteLine($"{result} books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4200)
                .ToArray();
            foreach (var book in books)
            {
                context.Books.Remove(book);
            }
            context.SaveChanges();
            return books.Length;
        }

        public static void IncreasePrices(BookShopContext db)
        {
            var books = db.Books
                .Where(x => x.ReleaseDate.Value.Year < 2010).ToArray();

            foreach (var book in books)
            {
                book.Price += 5m;
            }
            db.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();
            var categories = context.Categories
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Name,
                    Books = x.CategoryBooks
                        .Select(s => new
                        {
                            s.Book.Title,
                            s.Book.ReleaseDate
                        })
                        .OrderByDescending(f => f.ReleaseDate)
                        .Take(3)
                        .ToArray()
                })
                .ToArray();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            var category = context.Categories
                .Select(x => new
                {
                    x.Name,
                    TotalProfit = x.CategoryBooks.Sum(s => s.Book.Copies * s.Book.Price)
                })
                .OrderByDescending(x => x.TotalProfit)
                .ThenBy(x => x.Name)
                .ToArray();

            foreach (var c in category)
            {
                sb.AppendLine($"{c.Name} ${c.TotalProfit:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            var authors = context.Authors
                .Select(x => new
                {
                   FullName =  x.FirstName + " " + x.LastName,
                    TotalCopies = x.Books.Sum(c => c.Copies)
                })
                .OrderByDescending(x => x.TotalCopies)
                .ToArray();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.TotalCopies}");
            }    

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int input)
        {
            var countOfBooks = context.Books.Count(x => x.Title.Length > input);

            return countOfBooks;
        }

        public static string GetBooksByAuthor(BookShopContext context, string command)
        {
            var sb = new StringBuilder();
            var books = context.Books
                .Where(x => x.Author.LastName.ToLower().StartsWith(command.ToLower()))
                .OrderBy(x => x.BookId)
                .Select(x => new
                {
                    Title = x.Title,
                    FullName = x.Author.FirstName + " " + x.Author.LastName
                })
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FullName})");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string command)
        {
            var books = context.Books
                .Where(x => x.Title.ToLower().Contains(command.ToLower()))
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToArray();

            return string.Join(Environment.NewLine,books);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string command)
        {
            var sb = new StringBuilder();
            var authors = context.Authors
                //.Where(x => x.FirstName.EndsWith(command))
                .Where(f => EF.Functions.Like(f.FirstName,"%" + command))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToArray();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string command)
        {
            var sb = new StringBuilder();

            DateTime date = DateTime.ParseExact(command,"dd-MM-yyyy",CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(x => x.ReleaseDate < date)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => new
                {
                    Title = x.Title,
                    Type = x.EditionType,
                    Price = x.Price
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.Type} - ${book.Price:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public  static string GetBooksByCategory(BookShopContext context, string command)
        {
            var listOfCategories = command.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books
                .Where(x => x.BookCategories.Any(c => listOfCategories.Contains(c.Category.Name.ToLower())))
                .Select(x => x.Title)
                .OrderBy(x => x)                
                .ToArray();


            return string.Join(Environment.NewLine,books);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int command)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year != command)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);

        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(x => x.Price > 40.0m)
                .OrderByDescending(x => x.Price)
                .Select(x => new
                {
                    Title = x.Title,
                    Price = x.Price
                })
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var edType = (EditionType)Enum.Parse(typeof(EditionType), "Gold", true);
            var books = context.Books
                .Where(x => x.EditionType == edType && x.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var AgeRest = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);

            var booksAgeRes = context.Books
                .Where(x => x.AgeRestriction == AgeRest)
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, booksAgeRes);

            return result;
        }
    }
}
