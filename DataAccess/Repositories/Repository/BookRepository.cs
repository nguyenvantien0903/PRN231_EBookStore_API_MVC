using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repository
{
    public class BookRepository : IBookRepository
    {
        public void DeleteBook(Book p) => BookDAO.DeleteBook(p);

        public Book GetBookById(int id) => BookDAO.FindBookById(id);

        public List<Book> GetBooks() => BookDAO.GetBooks();

        public void SaveBook(Book p) => BookDAO.SaveBook(p);

        public void UpdateBook(Book p) => BookDAO.UpdateBook(p);
    }
}
