using FsCheck;
using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using Xunit;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class BookProperties
    {
        
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void NewBook_Initially_HasAvailableCopiesEqualTotal(Book book)
        {
            Assert.Equal(book.TotalCopies, book.AvailableCopies);
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Borrow_DecrementsAvailableCopies(Book book)
        {
            int initialCopies = book.AvailableCopies;
            book.Borrow();
            Assert.Equal(initialCopies - 1, book.AvailableCopies);
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Return_IncrementsAvailableCopies(Book book)
        {
            book.Borrow();
            int copiesAfterBorrow = book.AvailableCopies;
            book.Return();
            Assert.Equal(copiesAfterBorrow + 1, book.AvailableCopies);
        }
    }
}