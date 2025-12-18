using FsCheck;
using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using System;
using Xunit;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class ErrorProperties
    {
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Borrow_Throws_WhenNoCopiesAvailable(Book book)
        {
            // Вичерпуємо ліміт
            while (book.AvailableCopies > 0)
            {
                book.Borrow();
            }

            
            Assert.Throws<InvalidOperationException>(() => book.Borrow());
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Return_Throws_WhenAllCopiesArePresent(Book book)
        {
            
            Assert.Throws<InvalidOperationException>(() => book.Return());
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Service_ReturnBook_Throws_IfUserDidNotBorrow(User user, Book book)
        {
            var repo = new InMemoryLibraryRepository();
            repo.SaveUser(user);
            repo.SaveBook(book);
            var service = new LibraryService(repo);

           
            Assert.Throws<InvalidOperationException>(() => service.ReturnBook(user.Id, book.Isbn));
        }
    }
}