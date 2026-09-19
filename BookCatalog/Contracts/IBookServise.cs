using System;
using System.Collections.Generic;
using System.Text;
using BookCatalog.Models;

namespace BookCatalog.Contracts
{
    public interface IBookService
    {
        void AddBook(Book book);
        IEnumerable<Book> GetAllBooks();
        Book GetById(Guid id);
        void DeleteBook(Guid id);
    }
}
