using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rental : BaseEntity
{
    public Guid MemberId { get; set; }
    public Member Member { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<BookRental> Books { get; set; }
}
