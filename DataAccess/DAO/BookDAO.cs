using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookDAO
    {
        public static List<Book> GetBooks()
        {
            var listBooks = new List<Book>();
            try
            {
                using var context = new eBookStoreDBContext();
                listBooks = context.Books.Include(b => b.Publisher).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBooks;
        }

        public static Book FindBookById(int prodId)
        {
            Book p = new();
            try
            {
                using var context = new eBookStoreDBContext();
                p = context.Books.Include(b => b.Publisher).SingleOrDefault(x => x.BookId == prodId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SaveBook(Book Book)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Books.Add(Book);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateBook(Book Book)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Entry<Book>(Book).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteBook(Book Book)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                var p1 = context.Books.SingleOrDefault(
                    x => x.BookId == Book.BookId);
                context.Books.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
