using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interface
{
    public interface IBookRepository
    {
        void SaveBook(Book p);

        Book GetBookById(int id);

        void DeleteBook(Book p);

        void UpdateBook(Book p);

        List<Book> GetBooks();
    }
}
