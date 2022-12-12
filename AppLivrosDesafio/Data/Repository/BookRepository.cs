using AppLivrosDesafio.Data.Interfaces;
using AppLivrosDesafio.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppLivrosDesafio.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppLivrosDesafioContext context) : base(context) { }

        public IEnumerable<Book> FindWithUser(Func<Book, bool> predicate)
        {
            return _context.Books
                .Include(a => a.User)
                .Where(predicate);
        }
        public IEnumerable<Book> GetAllWithUser()
        {
            return _context.Books.Include(a => a.User);
        }
    }
}
