using AppLivrosDesafio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLivrosDesafio.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllWithBooks();
        User GetWithBooks(int id);
    }
}
