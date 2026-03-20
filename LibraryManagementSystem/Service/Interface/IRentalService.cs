using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IRentalService
    {
        List<Rental> GetAllRentals();
        Rental? GetRentalById(Guid id);
        Rental RentBook(Guid bookId, Guid memberId); // враќа креирано рентање
        void DeleteRental(Guid id);
    }
}
