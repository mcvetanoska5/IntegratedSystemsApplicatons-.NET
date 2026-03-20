using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementation
{
    public class RentalService : IRentalService
    {
        private readonly IRepository<Rental> _rentalRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Member> _memberRepository;
        public RentalService(IRepository<Rental> rentalRepository,
                             IRepository<Book> bookRepository,
                             IRepository<Member> memberRepository)
        {
            _rentalRepository = rentalRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }
        public List<Rental> GetAllRentals()
        {
            return _rentalRepository.GetAll(
                selector: x => x,
                include: source => source
                    .Include(r => r.Member)        
                    .Include(r => r.Books)        
                        .ThenInclude(br => br.Book) 
            ).ToList();
        }
        public Rental? GetRentalById(Guid id)
        {
            return _rentalRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id,
                include: source => source
                    .Include(r => r.Member)
                    .Include(r => r.Books)
                        .ThenInclude(br => br.Book)
            );
        }
        public Rental RentBook(Guid bookId, Guid memberId)
        {
            var book = _bookRepository.Get(x => x, x => x.Id == bookId);
            if (book == null || !book.IsAvailable)
                throw new Exception("Книгата не е достапна.");
            var member = _memberRepository.Get(x => x, x => x.Id == memberId);
            if (member == null)
                throw new Exception("Членот не е пронајден.");
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                MemberId = memberId,
                CreatedAt = DateTime.UtcNow,
                Books = new List<BookRental>()
            };
            var bookRental = new BookRental
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                RentalId = rental.Id,
                RentedAt = DateTime.UtcNow
            };
            rental.Books.Add(bookRental);
            _rentalRepository.Insert(rental);
            book.IsAvailable = false;
            _bookRepository.Update(book);

            return rental;
        }
        public void DeleteRental(Guid id)
        {
            var rental = _rentalRepository.Get(x => x, x => x.Id == id);
            if (rental == null) return;

            if (rental.Books != null)
            {
                foreach (var br in rental.Books)
                {
                    var book = _bookRepository.Get(x => x, x => x.Id == br.BookId);
                    if (book != null)
                    {
                        book.IsAvailable = true;
                        _bookRepository.Update(book);
                    }
                }
            }
            _rentalRepository.Delete(rental);
        }
    }
}
