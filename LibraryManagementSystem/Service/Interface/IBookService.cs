using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(Guid id);
        Book InsertBook(Book book);
        Book UpdateBook(Book book);
        Book DeleteBook(Guid id);
        // Дополнителна акција
        void RentBook(Guid bookId, Guid memberId);
    }
}
