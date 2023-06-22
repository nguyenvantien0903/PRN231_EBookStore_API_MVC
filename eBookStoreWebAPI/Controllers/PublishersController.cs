using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : Controller
    {
        private readonly IPublisherRepository _repo;

        public PublishersController(IPublisherRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Publisher>> GetPublishers() => _repo.GetPublishers();

        [HttpGet("GetById")]
        public ActionResult<Publisher> GetPublisherById(int id) => _repo.GetPublisherById(id);

        [HttpPost]
        public IActionResult PostPublisher(Publisher p)
        {
            _repo.SavePublisher(p);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeletePublisher(int id)
        {
            var p = _repo.GetPublisherById(id);
            if (p == null)
            {
                return NotFound();
            }
            _repo.DeletePublisher(p);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdatePublisher(int id, Publisher p)
        {
            var pTmp = _repo.GetPublisherById(id);
            if (pTmp == null)
            {
                return NotFound();
            }
            _repo.UpdatePublisher(p);
            return NoContent();
        }
    }
}
