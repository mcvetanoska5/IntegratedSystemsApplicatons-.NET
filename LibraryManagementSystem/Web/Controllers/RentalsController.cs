using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interface;
using Repository.Interface;
using Domain;

namespace Web.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Member> _memberRepository;

        public RentalsController(IRentalService rentalService, IRepository<Book> bookRepository, IRepository<Member> memberRepository)
        {
            _rentalService = rentalService;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        [HttpGet("api/rentals")]
        public IActionResult GetRentalsApi() => Ok(_rentalService.GetAllRentals());

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Rentals")]
        public IActionResult Index() => View(_rentalService.GetAllRentals());

        [HttpGet("api/rentals/{id}")]
        public IActionResult GetRentalApi(Guid id) => Ok(_rentalService.GetRentalById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Rentals/Details/{id}")]
        public IActionResult Details(Guid id) => View(_rentalService.GetRentalById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Rentals/Create")]
        public IActionResult Create()
        {
            ViewBag.BookList = new SelectList(_bookRepository.GetAll(x => x).Where(b => b.IsAvailable), "Id", "Title");
            ViewBag.MemberList = new SelectList(_memberRepository.GetAll(x => x), "Id", "FullName");
            return View();
        }

        [HttpPost("api/rentals")]
        public IActionResult CreateApi(Guid bookId, Guid memberId)
        {
            _rentalService.RentBook(bookId, memberId);
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Rentals/Create")]
        public IActionResult Create(Guid bookId, Guid memberId)
        {
            _rentalService.RentBook(bookId, memberId);
            return RedirectToAction(nameof(Index));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Rentals/Delete/{id}")]
        public IActionResult Delete(Guid id) => View(_rentalService.GetRentalById(id));

        [HttpDelete("api/rentals/{id}")]
        public IActionResult DeleteApi(Guid id) { _rentalService.DeleteRental(id); return NoContent(); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Rentals/DeleteConfirmed/{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _rentalService.DeleteRental(id);
            return RedirectToAction(nameof(Index));
        }
    }
}