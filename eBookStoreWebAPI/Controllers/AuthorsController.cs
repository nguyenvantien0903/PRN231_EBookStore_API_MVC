using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess;
using Microsoft.AspNetCore.OData.Query;
using DataAccess.Repositories;
using NuGet.Protocol.Core.Types;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _repo;

        public AuthorsController(IAuthorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Author>> GetAuthors() => _repo.GetAuthors();

        [HttpGet("GetById")]
        public ActionResult<Author> GetAuthorById(int id) => _repo.GetAuthorById(id);

        [HttpPost]
        public IActionResult PostAuthor(Author p)
        {
            _repo.SaveAuthor(p);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id)
        {
            var p = _repo.GetAuthorById(id);
            if (p == null)
            {
                return NotFound();
            }
            _repo.DeleteAuthor(p);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id, Author p)
        {
            var pTmp = _repo.GetAuthorById(id);
            if (pTmp == null)
            {
                return NotFound();
            }
            _repo.UpdateAuthor(p);
            return NoContent();
        }
    }
}
