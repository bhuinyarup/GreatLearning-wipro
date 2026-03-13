using NUnit.Framework;
using LibraryManagementSystem;

namespace LibraryManagementSystem.Tests
{
    public class BorrowerTests
    {
        [Test]
        public void BorrowBook_ShouldAddBookToBorrowedList()
        {
            var borrower = new Borrower("Harsh", "CARD1");
            var book = new Book("C#", "John", "111");

            borrower.BorrowBook(book);

            Assert.That(borrower.BorrowedBooks.Count, Is.EqualTo(1));
        }

        [Test]
        public void ReturnBook_ShouldRemoveBookFromBorrowedList()
        {
            var borrower = new Borrower("Harsh", "CARD1");
            var book = new Book("C#", "John", "111");

            borrower.BorrowBook(book);
            borrower.ReturnBook(book);

            Assert.That(borrower.BorrowedBooks.Count, Is.EqualTo(0));
        }
    }
}