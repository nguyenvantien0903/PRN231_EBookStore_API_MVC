using BusinessObject;
using DataAccess.Repositories.Interface;
using eBookStoreWebAPI.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : Controller
    {

        private readonly IUserRepository _repo;
        private IConfiguration config;

        public AuthenController(IUserRepository _repository, IConfiguration _config)
        {
            _repo = _repository;
            config = _config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult AuthenticateAuthen([Bind("Email_adress,Password")] User info)
        {
            if (info == null) return BadRequest();

            var data = _repo.AuthenticateUser(info.Email_adress, info.Password);

            if (data == null) return Unauthorized();

            var accessToken = JwtConfig.CreateToken(data, config);

            var response = new 
            {
                Email = data.Email_adress,
                AccessToken = accessToken
            };

            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([Bind("Email_adress,Password")] User info)
        {
            var data = _repo.GetUserByEmail(info.Email_adress);
            if (data != null)
            {
                return BadRequest();
            }
            else
            {
                var user = new User
                {
                    Email_adress = info.Email_adress,
                    Password = info.Password,
                    Source = "",
                    First_name = "",
                    Middle_name = "",
                    Last_name = "",
                    RoleId = 2,
                    PublisherId = 1,
                    Hire_date = DateTime.Now,
                };
                _repo.SaveUser(user);
                return Ok();
            }
        }
    }
}
