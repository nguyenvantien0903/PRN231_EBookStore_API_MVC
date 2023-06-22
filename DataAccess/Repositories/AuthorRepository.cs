using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository   
    {
        public void DeleteAuthor(Author p) => AuthorDAO.DeleteAuthor(p);

        public Author GetAuthorById(int id) => AuthorDAO.FindAuthorById(id);

        public List<Author> GetAuthors() => AuthorDAO.GetAuthors();

        public void SaveAuthor(Author p) => AuthorDAO.SaveAuthor(p);

        public void UpdateAuthor(Author p) => AuthorDAO.UpdateAuthor(p);
    }
}
