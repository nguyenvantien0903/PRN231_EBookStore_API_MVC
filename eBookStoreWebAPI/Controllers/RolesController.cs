﻿using BusinessObject;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRoleRepository _repo;

        public RolesController(IRoleRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Role>> GetRoles() => _repo.GetRoles();

        [HttpGet("GetById")]
        public ActionResult<Role> GetRoleById(int id) => _repo.GetRoleById(id);

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult PostRole(Role p)
        {
            _repo.SaveRole(p);
            return NoContent();
        }

        [HttpDelete("id")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteRole(int id)
        {
            var p = _repo.GetRoleById(id);
            if (p == null)
            {
                return NotFound();
            }
            _repo.DeleteRole(p);
            return NoContent();
        }

        [HttpPut("id")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult UpdateRole(int id, Role p)
        {
            var pTmp = _repo.GetRoleById(id);
            if (pTmp == null)
            {
                return NotFound();
            }
            _repo.UpdateRole(p);
            return NoContent();
        }
    }
}
