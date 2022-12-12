using AppLivrosDesafio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLivrosDesafio.Data.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAllWithUser();
        IEnumerable<Book> FindWithUser(Func<Book, bool> predicate);
        
    }
}
