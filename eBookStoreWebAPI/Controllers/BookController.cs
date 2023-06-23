using BusinessObject;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repo;

        public BooksController(IBookRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Book>> GetBooks() => _repo.GetBooks();

        [HttpGet("GetById")]
        public ActionResult<Book> GetBookById(int id) => _repo.GetBookById(id);

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult PostBook(Book p)
        {
            _repo.SaveBook(p);
            return NoContent();
        }

        [HttpDelete("id")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteBook(int id)
        {
            var p = _repo.GetBookById(id);
            if (p == null)
            {
                return NotFound();
            }
            _repo.DeleteBook(p);
            return NoContent();
        }

        [HttpPut("id")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult UpdateBook(int id, Book p)
        {
            var pTmp = _repo.GetBookById(id);
            if (pTmp == null)
            {
                return NotFound();
            }
            _repo.UpdateBook(p);
            return NoContent();
        }
    }
}
