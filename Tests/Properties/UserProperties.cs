using FsCheck;
using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using System;
using Xunit;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class UserProperties
    {
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void NewUser_HasNoBorrowedBooks(User user)
        {
            Assert.Empty(user.BorrowedIsbns);
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void User_Id_IsNotEmpty(User user)
        {
            Assert.NotEqual(Guid.Empty, user.Id);
        }
    }
}