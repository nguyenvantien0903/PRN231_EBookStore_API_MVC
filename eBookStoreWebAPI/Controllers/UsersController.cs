using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<User>> GetUsers() => _repo.GetUsers();

        [HttpGet("GetById")]
        public ActionResult<User> GetUserById(int id) => _repo.GetUserById(id);

        [HttpPost]
        public IActionResult PostUser(User p)
        {
            _repo.SaveUser(p);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteUser(int id)
        {
            var p = _repo.GetUserById(id);
            if (p == null)
            {
                return NotFound();
            }
            _repo.DeleteUser(p);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateUser(int id, User p)
        {
            var pTmp = _repo.GetUserById(id);
            if (pTmp == null)
            {
                return NotFound();
            }
            _repo.UpdateUser(p);
            return NoContent();
        }
    }
}
