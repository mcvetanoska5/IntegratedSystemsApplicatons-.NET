using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Author : BaseEntity
{
    [Required]
    public string? FullName { get; set; }
    public virtual ICollection<Book>? Books { get; set; }
}
