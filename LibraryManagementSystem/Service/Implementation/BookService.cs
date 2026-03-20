using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Rental> _rentalRepository;
        private readonly IRepository<BookRental> _bookRentalRepository;

        public BookService(IRepository<Book> bookRepository,
                           IRepository<Rental> rentalRepository,
                           IRepository<BookRental> bookRentalRepository)
        {
            _bookRepository = bookRepository;
            _rentalRepository = rentalRepository;
            _bookRentalRepository = bookRentalRepository;
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll(x => x).ToList();
        }
        public Book? GetBookById(Guid id)
        {
            return _bookRepository.Get(x => x, x => x.Id == id);
        }
        public Book InsertBook(Book book)
        {
            book.Id = Guid.NewGuid();
            return _bookRepository.Insert(book);
        }
        public Book UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }
        public Book DeleteBook(Guid id)
        {
            var book = GetBookById(id);
            if (book == null) throw new Exception("Book not found");
            _bookRepository.Delete(book);
            return book;
        }
        public void RentBook(Guid bookId, Guid memberId)
        {
            var book = GetBookById(bookId);
            if (book == null || !book.IsAvailable)
                throw new Exception("Book is not available");
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                MemberId = memberId,
                CreatedAt = DateTime.UtcNow
            };
            _rentalRepository.Insert(rental);
            var bookRental = new BookRental
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                RentalId = rental.Id,
                RentedAt = DateTime.UtcNow
            };
            _bookRentalRepository.Insert(bookRental);
            book.IsAvailable = false;
            _bookRepository.Update(book);
        }
    }
}
