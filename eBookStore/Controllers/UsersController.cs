using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eBookStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public UsersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7298/api/Users";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<User> listProducts = JsonSerializer.Deserialize<List<User>>(strData, options);
            return View(listProducts);
        }

        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        public async Task LoadRoleAndPublisher()
        {
            string CategoriesApiUrl = "https://localhost:7298/api/Roles";
            HttpResponseMessage response2 = await client.GetAsync(CategoriesApiUrl);
            string strData2 = await response2.Content.ReadAsStringAsync();

            var options2 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Role> roles = JsonSerializer.Deserialize<List<Role>>(strData2, options2);
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "Role_desc");



            CategoriesApiUrl = "https://localhost:7298/api/Publishers";
            response2 = await client.GetAsync(CategoriesApiUrl);
            strData2 = await response2.Content.ReadAsStringAsync();

            options2 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(strData2, options2);
            ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "Publisher_name");
            return;
        }

        public async Task<IActionResult> Create()
        {
            await LoadRoleAndPublisher();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email_adress,Password,First_name,Middle_name,Last_name,Hire_date,RoleId,PublisherId")] User user)
        {
            if (ModelState.IsValid)
            {
                ProductApiUrl = "https://localhost:7298/api/Users";
                HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductApiUrl = "https://localhost:7298/api/Users/GetById?id=" + id;
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            User user = JsonSerializer.Deserialize<User>(strData, options);
            if (user == null)
            {
                return NotFound();
            }
            await LoadRoleAndPublisher();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Email_adress,Password,First_name,Middle_name,Last_name,Hire_date,RoleId,PublisherId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductApiUrl = "https://localhost:7298/api/Users/id?id=" + id;
                    HttpResponseMessage response = await client.PutAsJsonAsync(ProductApiUrl, user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ProductExists(product.ProductId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductApiUrl = "https://localhost:7298/api/Users/GetById?id=" + id;
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            User user = JsonSerializer.Deserialize<User>(strData, options);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ProductApiUrl = "https://localhost:7298/api/Users/id?id=" + id;
            HttpResponseMessage response = await client.DeleteAsync(ProductApiUrl);
            return RedirectToAction(nameof(Index));
        }

        //private bool UserExists(int id)
        //{
        //  return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
