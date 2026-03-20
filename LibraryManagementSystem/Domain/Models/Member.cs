using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Member : BaseEntity
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }
}
