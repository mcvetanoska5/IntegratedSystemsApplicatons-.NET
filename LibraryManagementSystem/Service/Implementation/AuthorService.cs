using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public List<Author> GetAllAuthors()
        {
            return _authorRepository.GetAll(x => x).ToList();
        }
        public Author? GetAuthorById(Guid id)
        {
            return _authorRepository.Get(x => x, x => x.Id == id);
        }
        public Author InsertAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            return _authorRepository.Insert(author);
        }
        public Author UpdateAuthor(Author author)
        {
            return _authorRepository.Update(author);
        }
        public Author DeleteAuthor(Guid id)
        {
            var author = GetAuthorById(id);
            if (author == null) throw new Exception("Author not found");
            _authorRepository.Delete(author);
            return author;
        }
    }
}
