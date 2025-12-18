using FsCheck;
using FsCheck.Fluent;
using Library_Book_Borrowing_System.Domain;
using System;
using System.Linq;

namespace Library_Book_Borrowing_System.Tests.Arbitraries
{
    public class LibraryArbitraries
    {
        // Генератор ISBN
        public static Arbitrary<string> Isbn()
        {
            return Gen.Choose(1000, 9999)
                      .Select(i => $"ISBN-{i}")
                      .ToArbitrary();
        }

        // Допоміжний генератор назв
        private static Gen<string> Title()
        {
            return Gen.Elements("Clean Code", "Refactoring", "Design Patterns", "The Hobbit", "1984");
        }

        // Генератор валідної книги
        public static Arbitrary<Book> Book()
        {
            
            var gen = from isbn in Isbn().Generator
                      from title in Title()
                      from copies in Gen.Choose(1, 50)
                      select new Book(isbn, title, copies);

            return gen.ToArbitrary();
        }

        // Генератор користувача
        public static Arbitrary<User> User()
        {
            var gen = from name in Gen.Elements("Alice", "Bob", "Charlie", "Dave")
                      select new User(name);

            return gen.ToArbitrary();
        }
    }
}
