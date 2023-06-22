using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AuthorDAO
    {
        public static List<Author> GetAuthors()
        {
            var listAuthors = new List<Author>();
            try
            {
                using var context = new eBookStoreDBContext();
                listAuthors = context.Authors.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listAuthors;
        }

        public static Author FindAuthorById(int prodId)
        {
            Author p = new();
            try
            {
                using var context = new eBookStoreDBContext();
                p = context.Authors.SingleOrDefault(x => x.AuthorId == prodId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SaveAuthor(Author Author)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Authors.Add(Author);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateAuthor(Author Author)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Entry<Author>(Author).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteAuthor(Author Author)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                var p1 = context.Authors.SingleOrDefault(
                    x => x.AuthorId == Author.AuthorId);
                context.Authors.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
