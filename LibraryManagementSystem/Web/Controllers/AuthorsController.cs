using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService) => _authorService = authorService;

        [HttpGet("/api/authors")]
        public IActionResult GetAuthorsApi() => Ok(_authorService.GetAllAuthors());

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("")]
        public IActionResult Index() => View(_authorService.GetAllAuthors());

        [HttpGet("/api/authors/{id}")]
        public IActionResult GetAuthorApi(Guid id) => Ok(_authorService.GetAuthorById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Details/{id}")]
        public IActionResult Details(Guid id) => View(_authorService.GetAuthorById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("/api/authors")]
        public IActionResult CreateApi(Author author)
        {
            _authorService.InsertAuthor(author);
            return Ok(author);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Create")]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid) { _authorService.InsertAuthor(author); return RedirectToAction(nameof(Index)); }
            return View(author);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(Guid id) => View(_authorService.GetAuthorById(id));

        [HttpPut("/api/authors/{id}")]
        public IActionResult EditApi(Guid id, Author author) { _authorService.UpdateAuthor(author); return Ok(author); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Guid id, Author author)
        {
            if (ModelState.IsValid) { _authorService.UpdateAuthor(author); return RedirectToAction(nameof(Index)); }
            return View(author);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(Guid id) => View(_authorService.GetAuthorById(id));

        [HttpDelete("/api/authors/{id}")]
        public IActionResult DeleteApi(Guid id) { _authorService.DeleteAuthor(id); return NoContent(); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id) { _authorService.DeleteAuthor(id); return RedirectToAction(nameof(Index)); }
    }
}