using BookCatalog.Contracts;
using BookCatalog.Exceptions;
using BookCatalog.Models;
using BookCatalog.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookTest
{
    [TestClass]
    public class BookServiceTests
    {
        private IBookService _bookService;

        [TestInitialize]
        public void Setup()
        {
            _bookService = new BookService();
        }
        [TestMethod]
        public void AddAndGetBook_ShouldReturnCorrectBook()
        {
            var bookId = Guid.NewGuid();
            var book = new Book(bookId, "Война и мир", "Лев Толстой", 1869);
            _bookService.AddBook(book);
            var result = _bookService.GetById(bookId);
            Assert.IsNotNull(result);
            Assert.AreEqual("Война и мир", result.Title);
            Assert.AreEqual(1869, result.Year);
        }
        [TestMethod]
        public void GetAllBooks_ShouldReturnCollectionWithAddedItems()
        {
            var book1 = new Book(Guid.NewGuid(), "Мастер и Маргарита", "Михаил Булгаков", 1967);
            var book2 = new Book(Guid.NewGuid(), "Преступление и наказание", "Федор Достоевский", 1866);

            _bookService.AddBook(book1);
            _bookService.AddBook(book2);

            var books = _bookService.GetAllBooks();

            Assert.AreEqual(2, books.Count());
        }
        [TestMethod]
        public void Manual_ExceptionHandling_FlowControl()
        {
            var service = new BookService();
            var validBook = new Book(Guid.NewGuid(), "Чистый код", "Роберт Мартин", 2008);
            Console.WriteLine("Начало выполнения теста ручной обработки исключений.");
            try
            {
                var invalidBook = new Book(Guid.NewGuid(), "", "Роберт Мартин", 2008);
                service.AddBook(invalidBook);
            }
            catch (InvalidParameterException ex)
            {
                Assert.AreEqual(nameof(Book.Title), ex.ParamName);
                Assert.AreEqual("Название книги не может быть пустым.", ex.Message);
                Console.WriteLine($"Исключение успешно перехвачено: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally выполнен. Состояние системы стабильно.");
            }
            service.AddBook(validBook);
            Assert.AreEqual(1, service.GetAllBooks().Count());
            Console.WriteLine("Программа корректно продолжила выполнение после обработки ошибки.");
        }
    }
}
