using BookCatalog.Contracts;
using BookCatalog.Exceptions;
using BookCatalog.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookCatalog.Services
{
    public class BookService : IBookService
    {
        private readonly ConcurrentDictionary<Guid, Book> _books = new ConcurrentDictionary<Guid, Book>();
        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            var currentYear = DateTime.UtcNow.Year;
            if (book.Year < 1450 || book.Year > currentYear + 1)
            {
                throw new ArgumentOutOfRangeException(nameof(book.Year), "Год издания книги некорректен.");
            }
            if (!_books.TryAdd(book.Id, book))
            {
                throw new InvalidOperationException($"Книга с идентификатором '{book.Id}' уже существует.");
            }
         
            if (book is null)
                throw new InvalidParameterException(nameof(book), "Объект книги не может быть равен null.");

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new InvalidParameterException(nameof(book.Title), "Название книги не может быть пустым.");

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new InvalidParameterException(nameof(book.Author), "Автор книги не может быть пустым.");
        }
        public IEnumerable<Book> GetAllBooks()
        {
            return _books.Values.ToList().AsReadOnly();
        }
        public Book GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Идентификатор не может быть пустым.", nameof(id));
            if (_books.TryGetValue(id, out var book))
            {
                return book;
            }
            throw new BookNotFoundException(id);
        }
        public void DeleteBook(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Идентификатор не может быть пустым.", nameof(id));
            if (!_books.TryRemove(id, out _))
                throw new BookNotFoundException(id);
        }

    }
}
