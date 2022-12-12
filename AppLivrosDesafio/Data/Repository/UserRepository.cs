using AppLivrosDesafio.Data.Interfaces;
using AppLivrosDesafio.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using AppLivrosDesafio.Data;


namespace AppLivrosDesafio.Data.Repository
{
    /************************************ // 
    O Repository para herdar o bd context para conexão, recebe a action do Controller e executa - 08/12 - YAGO
  *************************************/

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppLivrosDesafioContext context) : base(context) {}

        public IEnumerable<User> GetAllWithBooks()
        {
            return _context.Users.Include(a => a.Books);
        }

        public User GetWithBooks(int id)
        {
            return _context.Users.Where(a => a.UserId == id).Include(a => a.Books).FirstOrDefault();
        }

        public override void Delete(User entity)
        {
            var booksToRemove = _context.Books.Where(b => b.User == entity);

            base.Delete(entity);

            _context.Books.RemoveRange(booksToRemove);

            Save();
        }
    }
}
