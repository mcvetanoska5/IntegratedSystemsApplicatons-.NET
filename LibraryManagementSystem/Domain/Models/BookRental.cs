using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BookRental : BaseEntity
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid RentalId { get; set; }
    public Rental Rental { get; set; }
    public DateTime RentedAt { get; set; }
}
