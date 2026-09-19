using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Exceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(Guid bookId)
            : base($"Книга с идентификатором '{bookId}' не найдена.")
        {
        }
    }
}
