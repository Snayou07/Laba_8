using FsCheck;
using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using Xunit;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class SequenceProperties
    {
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Borrow_Then_Return_RestoresState(User user, Book book)
        {
            // Arrange
            var repo = new InMemoryLibraryRepository();
            repo.SaveBook(book);
            repo.SaveUser(user);
            var service = new LibraryService(repo);

            int initialCopies = book.AvailableCopies;

            // Act 1: Borrow
            service.BorrowBook(user.Id, book.Isbn);

            // Check 1
            var userAfterBorrow = repo.GetUser(user.Id);
            Assert.Contains(book.Isbn, userAfterBorrow.BorrowedIsbns);

            // Act 2: Return
            service.ReturnBook(user.Id, book.Isbn);

            // Assert Final
            var userFinal = repo.GetUser(user.Id);
            var bookFinal = repo.GetBook(book.Isbn);

            Assert.DoesNotContain(book.Isbn, userFinal.BorrowedIsbns);
            Assert.Equal(initialCopies, bookFinal.AvailableCopies);
        }
    }
}