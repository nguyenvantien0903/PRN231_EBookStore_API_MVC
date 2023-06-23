using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace eBookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public BooksController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7298/api/Books";
        }

        private void SetAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task LoaddPublisher()
        {
            //string CategoriesApiUrl = "https://localhost:7298/api/Roles";
            //HttpResponseMessage response2 = await client.GetAsync(CategoriesApiUrl);
            //string strData2 = await response2.Content.ReadAsStringAsync();

            //var options2 = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true,
            //};
            //List<Role> roles = JsonSerializer.Deserialize<List<Role>>(strData2, options2);
            //ViewData["RoleId"] = new SelectList(roles, "RoleId", "Role_desc");



            string CategoriesApiUrl = "https://localhost:7298/api/Publishers";
            SetAuthorizationHeader();
            HttpResponseMessage response2 = await client.GetAsync(CategoriesApiUrl);
            string strData2 = await response2.Content.ReadAsStringAsync();

            var options2 = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(strData2, options2);
            ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "Publisher_name");
            return;
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            SetAuthorizationHeader();
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Book> listProducts = JsonSerializer.Deserialize<List<Book>>(strData, options);
            return View(listProducts);
        }

        public async Task<IActionResult> Search(string searchTitle)
        {
            SetAuthorizationHeader();
            ProductApiUrl = "https://localhost:7298/odata/Books?$filter=contains(Title,'"+ searchTitle + "')";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic jsonObject = JsonObject.Parse(strData);

            List<Book> books = ((JsonArray)jsonObject["value"]).Select(x => new Book
            {
                BookId = (int)x["BookId"],
                Title = (string)x["Title"],
                Type = (string)x["Type"],
                Price = (decimal)x["Price"],
                Advance = (decimal)x["Advance"],
                Royalty = (decimal)x["Royalty"],
                Ytd_sales = (decimal)x["Ytd_sales"],
                Notes = (string)x["Notes"],
                Published_date = (DateTime)x["Published_date"],
                PublisherId = (int)x["PublisherId"],
            }).ToList();
            return View("Index", books);
        }

        //// GET: Books/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Books == null)
        //    {
        //        return NotFound();
        //    }

        //    var author = await _context.Books
        //        .FirstOrDefaultAsync(m => m.BookId == id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(author);
        //}

        public async Task<IActionResult> Create()
        {
            await LoaddPublisher();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Type,PublisherId,Price,Notes,Published_date")] Book author)
        {
            if (ModelState.IsValid)
            {
                ProductApiUrl = "https://localhost:7298/api/Books";
                SetAuthorizationHeader();
                HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductApiUrl = "https://localhost:7298/api/Books/GetById?id=" + id;
            SetAuthorizationHeader();
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Book author = JsonSerializer.Deserialize<Book>(strData, options);
            if (author == null)
            {
                return NotFound();
            }
            await LoaddPublisher();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Type,PublisherId,Price,Notes,Published_date")] Book author)
        {
            if (id != author.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductApiUrl = "https://localhost:7298/api/Books/id?id=" + id;
                    SetAuthorizationHeader();
                    HttpResponseMessage response = await client.PutAsJsonAsync(ProductApiUrl, author);
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
            return View(author);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductApiUrl = "https://localhost:7298/api/Books/GetById?id=" + id;
            SetAuthorizationHeader();
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Book author = JsonSerializer.Deserialize<Book>(strData, options);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ProductApiUrl = "https://localhost:7298/api/Books/id?id=" + id;
            SetAuthorizationHeader();
            HttpResponseMessage response = await client.DeleteAsync(ProductApiUrl);
            return RedirectToAction(nameof(Index));
        }

        //private bool BookExists(int id)
        //{
        //  return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        //}
    }
}

