using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eBookStore.Controllers
{
    public class PublishersController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public PublishersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7298/api/Publishers";
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
            List<Publisher> listProducts = JsonSerializer.Deserialize<List<Publisher>>(strData, options);
            return View(listProducts);
        }

        //// GET: Publishers/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Publishers == null)
        //    {
        //        return NotFound();
        //    }

        //    var author = await _context.Publishers
        //        .FirstOrDefaultAsync(m => m.PublisherId == id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(author);
        //}

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,Publisher_name,City,Country")] Publisher author)
        {
            if (ModelState.IsValid)
            {
                ProductApiUrl = "https://localhost:7298/api/Publishers";
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

            ProductApiUrl = "https://localhost:7298/api/Publishers/GetById?id=" + id;
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Publisher author = JsonSerializer.Deserialize<Publisher>(strData, options);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Publisher_name,City,Country")] Publisher author)
        {
            if (id != author.PublisherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductApiUrl = "https://localhost:7298/api/Publishers/id?id=" + id;
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

            ProductApiUrl = "https://localhost:7298/api/Publishers/GetById?id=" + id;
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Publisher author = JsonSerializer.Deserialize<Publisher>(strData, options);
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
            ProductApiUrl = "https://localhost:7298/api/Publishers/id?id=" + id;
            HttpResponseMessage response = await client.DeleteAsync(ProductApiUrl);
            return RedirectToAction(nameof(Index));
        }

        //private bool PublisherExists(int id)
        //{
        //  return (_context.Publishers?.Any(e => e.PublisherId == id)).GetValueOrDefault();
        //}
    }
}

