using NUnit.Framework;
using LibraryManagementSystem;
using System;

namespace LibraryManagementSystem.Tests
{
    public class LibraryTests
    {
        private Library _library;

        [SetUp]
        public void Setup()
        {
            _library = new Library();
        }

        [Test]
        public void AddBook_ShouldAddBook()
        {
            var book = new Book("C#", "John", "111");

            _library.AddBook(book);

            Assert.That(_library.Books.Count, Is.EqualTo(1));
        }

        [Test]
        public void RegisterBorrower_ShouldAddBorrower()
        {
            var borrower = new Borrower("Harsh", "CARD1");

            _library.RegisterBorrower(borrower);

            Assert.That(_library.Borrowers.Count, Is.EqualTo(1));
        }

        [Test]
        public void BorrowBook_ShouldMarkBookAsBorrowed()
        {
            var book = new Book("C#", "John", "111");
            var borrower = new Borrower("Harsh", "CARD1");

            _library.AddBook(book);
            _library.RegisterBorrower(borrower);

            _library.BorrowBook("111", "CARD1");

            Assert.That(book.IsBorrowed, Is.True);
            Assert.That(borrower.BorrowedBooks.Count, Is.EqualTo(1));
        }

        [Test]
        public void ReturnBook_ShouldMarkBookAsAvailable()
        {
            var book = new Book("C#", "John", "111");
            var borrower = new Borrower("Harsh", "CARD1");

            _library.AddBook(book);
            _library.RegisterBorrower(borrower);
            _library.BorrowBook("111", "CARD1");

            _library.ReturnBook("111", "CARD1");

            Assert.That(book.IsBorrowed, Is.False);
        }

        [Test]
        public void BorrowBook_WithInvalidISBN_ShouldThrowException()
        {
            var borrower = new Borrower("Harsh", "CARD1");
            _library.RegisterBorrower(borrower);

            Assert.Throws<InvalidOperationException>(() =>
                _library.BorrowBook("INVALID", "CARD1"));
        }
    }
}