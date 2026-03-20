using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
        Author? GetAuthorById(Guid id);
        Author InsertAuthor(Author author);
        Author UpdateAuthor(Author author);
        Author DeleteAuthor(Guid id);
    }
}
