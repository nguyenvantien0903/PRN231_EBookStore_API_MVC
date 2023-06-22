﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eBookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public AuthorsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7298/api/Authors";
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
            List<Author> listProducts = JsonSerializer.Deserialize<List<Author>>(strData, options);
            return View(listProducts);
        }

        //// GET: Authors/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Authors == null)
        //    {
        //        return NotFound();
        //    }

        //    var author = await _context.Authors
        //        .FirstOrDefaultAsync(m => m.AuthorId == id);
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
        public async Task<IActionResult> Create([Bind("AuthorId,First_name,Last_name,City,Email_address")] Author author)
        {
            if (ModelState.IsValid)
            {
                ProductApiUrl = "https://localhost:7298/api/Authors";
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

            ProductApiUrl = "https://localhost:7298/api/Authors/GetById?id=" + id;
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Author author = JsonSerializer.Deserialize<Author>(strData, options);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,First_name,Last_name,Email_address,City")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductApiUrl = "https://localhost:7298/api/Authors/id?id=" + id;
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

            ProductApiUrl = "https://localhost:7298/api/Authors/GetById?id=" + id;
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Author author = JsonSerializer.Deserialize<Author>(strData, options);
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
            ProductApiUrl = "https://localhost:7298/api/Authors/id?id=" + id;
            HttpResponseMessage response = await client.DeleteAsync(ProductApiUrl);
            return RedirectToAction(nameof(Index));
        }

        //private bool AuthorExists(int id)
        //{
        //  return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        //}
    }
}
