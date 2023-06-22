using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IAuthorRepository
    {
        void SaveAuthor(Author p);

        Author GetAuthorById(int id);

        void DeleteAuthor(Author p);

        void UpdateAuthor(Author p);

        List<Author> GetAuthors();

    }
}
