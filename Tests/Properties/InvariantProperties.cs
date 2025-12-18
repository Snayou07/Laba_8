using FsCheck;
using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using Xunit;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class InvariantProperties
    {
        
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Book_AvailableCopies_NeverExceedsTotal(Book book)
        {
            
            try { book.Return(); } catch { }

            Assert.True(book.AvailableCopies <= book.TotalCopies,
                $"Available ({book.AvailableCopies}) > Total ({book.TotalCopies})");
        }

       
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Book_AvailableCopies_NeverNegative(Book book)
        {
            for (int i = 0; i < book.TotalCopies + 5; i++)
            {
                try { book.Borrow(); } catch { }
            }

            Assert.True(book.AvailableCopies >= 0);
        }
    }
}