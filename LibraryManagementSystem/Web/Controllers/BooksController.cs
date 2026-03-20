using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interface;
using Service.Implementation;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly BookApiService _bookApiService;

        public BooksController(IBookService bookService, IAuthorService authorService, BookApiService bookApiService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _bookApiService = bookApiService;
        }

        [HttpGet("/api/books")]
        public IActionResult GetBooksApi() => Ok(_bookService.GetAllBooks());

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("")]
        public IActionResult Index() => View(_bookService.GetAllBooks());

        [HttpGet("/api/books/{id}")]
        public IActionResult GetBookApi(Guid id) => Ok(_bookService.GetBookById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Details/{id}")]
        public IActionResult Details(Guid id) => View(_bookService.GetBookById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Create")]
        public IActionResult Create() { PopulateAuthorsSelectList(); return View(); }

        [HttpPost("/api/books")]
        public IActionResult CreateApi(Book book) { _bookService.InsertBook(book); return Ok(book); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Create")]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid) { _bookService.InsertBook(book); return RedirectToAction(nameof(Index)); }
            PopulateAuthorsSelectList(); return View(book);
        }

        [HttpPost("/api/books/import")]
        public async Task<IActionResult> ImportByISBN([FromQuery] string isbn) => Ok(await _bookApiService.GetBookByISBN(isbn));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(Guid id) { PopulateAuthorsSelectList(); return View(_bookService.GetBookById(id)); }

        [HttpPut("/api/books/{id}")]
        public IActionResult EditApi(Guid id, Book book) { _bookService.UpdateBook(book); return Ok(book); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Guid id, Book book)
        {
            if (ModelState.IsValid) { _bookService.UpdateBook(book); return RedirectToAction(nameof(Index)); }
            PopulateAuthorsSelectList(); return View(book);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(Guid id) => View(_bookService.GetBookById(id));

        [HttpDelete("/api/books/{id}")]
        public IActionResult DeleteApi(Guid id) { _bookService.DeleteBook(id); return NoContent(); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id) { _bookService.DeleteBook(id); return RedirectToAction(nameof(Index)); }

        private void PopulateAuthorsSelectList() => ViewBag.AuthorList = new SelectList(_authorService.GetAllAuthors(), "Id", "FullName");
    }
}