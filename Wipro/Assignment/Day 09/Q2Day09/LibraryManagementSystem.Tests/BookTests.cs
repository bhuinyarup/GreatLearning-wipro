using NUnit.Framework;
using LibraryManagementSystem;
using System;

namespace LibraryManagementSystem.Tests
{
    public class BookTests
    {
        [Test]
        public void Borrow_ShouldMarkBookAsBorrowed()
        {
            var book = new Book("C#", "John", "111");

            book.Borrow();

            Assert.That(book.IsBorrowed, Is.True);
        }

        [Test]
        public void Borrow_WhenAlreadyBorrowed_ShouldThrowException()
        {
            var book = new Book("C#", "John", "111");
            book.Borrow();

            Assert.Throws<InvalidOperationException>(() => book.Borrow());
        }

        [Test]
        public void Return_ShouldMarkBookAsAvailable()
        {
            var book = new Book("C#", "John", "111");
            book.Borrow();

            book.Return();

            Assert.That(book.IsBorrowed, Is.False);
        }
    }
}