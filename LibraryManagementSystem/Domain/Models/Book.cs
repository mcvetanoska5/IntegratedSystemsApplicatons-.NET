using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Book : BaseEntity
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? ISBN { get; set; }
    public bool IsAvailable { get; set; } = true;
    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }
    public virtual ICollection<BookRental>? BookRentals { get; set; }
}