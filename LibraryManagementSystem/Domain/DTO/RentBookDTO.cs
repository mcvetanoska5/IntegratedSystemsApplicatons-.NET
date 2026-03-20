using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RentBookDTO
{
    public Guid SelectedBookId { get; set; }
    public Guid SelectedMemberID { get; set; }
    public string SelectedBookTitle { get; set; }
}
