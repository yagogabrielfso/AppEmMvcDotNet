using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppLivrosDesafio.Data.Interfaces;
using AppLivrosDesafio.ViewModels;
using AppLivrosDesafio.Data.Models;
using Aspose.BarCode.Generation;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppLivrosDesafio.Controllers
{
    
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment  _env;

        public BookController(IBookRepository repository, IUserRepository userRepository, IWebHostEnvironment env)
        {
            _repository = repository;
            _userRepository = userRepository;
            _env = env;
        }
        [Route("Book")]
        public IActionResult List(int? userId)
        {

            var book = _repository.GetAllWithUser().ToList();

            IEnumerable<Book> books;

            ViewBag.UserId = userId;

           

            if (userId == null)
            {
                books = _repository.GetAllWithUser();
                return CheckBooksCount(books);
            }
            else
            {
                var user = _userRepository.GetWithBooks((int)userId);

                if (user.Books == null || user.Books.Count == 0)
                    return View("EmptyUser", user);
            }

            books = _repository.FindWithUser(a => a.User.UserId == userId);

            return CheckBooksCount(books);
        }

        //Não está do jeito de ideal a ordenação, fiz assim só pela funcionalidade - 09/12 - YAGO
        public IActionResult ListOrderAZ(int? userId)
        {

            var book = _repository.GetAllWithUser().ToList();
            
            IEnumerable<Book> books;

            ViewBag.UserId = userId;



            if (userId == null)
            {
                books = _repository.GetAllWithUser();
                return CheckBooksCount(books);
            }
            else
            {
                var user = _userRepository.GetWithBooks((int)userId);

                if (user.Books == null || user.Books.Count == 0)
                    return View("EmptyUser", user);
            }

            books = _repository.FindWithUser(a => a.User.UserId == userId);

            return CheckBooksCount(books);
        }

        public IActionResult ListOrderZA(int? userId)
        {

            var book = _repository.GetAllWithUser().ToList();

            IEnumerable<Book> books;

            ViewBag.UserId = userId;



            if (userId == null)
            {
                books = _repository.GetAllWithUser();
                return CheckBooksCount(books);
            }
            else
            {
                var user = _userRepository.GetWithBooks((int)userId);

                if (user.Books == null || user.Books.Count == 0)
                    return View("EmptyUser", user);
            }

            books = _repository.FindWithUser(a => a.User.UserId == userId);

            return CheckBooksCount(books);
        }
        private IActionResult CheckBooksCount(IEnumerable<Book> books)
        {
            if (books == null || books.ToList().Count == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(books);
            }
        }

        public IActionResult ViewBook(int id)
        {
            Book book = _repository.FindWithUser(a => a.BookId == id).FirstOrDefault();
           // book.Image = this.GetImage(Convert.ToBase64String(book.Image));

            if (book == null)
            {
                return NotFound();
            }

            var bookVM = new BookViewModel
            {
                Book = book,
                Users = _userRepository.GetAll()
            };

            return View(bookVM);
        }

        //public byte[] GetImage(string sBase64String)
        //{
        //    byte[] bytes = null;
        //    if (!string.IsNullOrEmpty(sBase64String)) 
        //    {
        //        bytes = Convert.FromBase64String(sBase64String);
        //    }
        //    return bytes;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(BookViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                bookVM.Users = _userRepository.GetAll();
                return View(bookVM);
            }
            _repository.Update(bookVM.Book);

            return RedirectToAction("List");
        }

        public IActionResult Create(int? userId)
        {
            Book book = new Book();

            if(userId != null)
            {
                book.UserId = (int)userId;
            }

            var bookVM = new BookViewModel
            {
                Users = _userRepository.GetAll(),
                Book = book
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookViewModel bookVM)
        {             
            
            if (!ModelState.IsValid)
            {
                
                
                bookVM.Users = _userRepository.GetAll();
                return View(bookVM);
            }

            bookVM.Book.ISBN = 978123456789;
           // SaveFile();
            _repository.Create(bookVM.Book);

            return RedirectToAction("List");
        }
        public IActionResult Delete(int id, int? userId)
        {
            var book = _repository.GetById(id);

            _repository.Delete(book);

            return RedirectToAction("List", new { userId = userId });
        }



        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult ViewImage(int id)
        //{
        //    var item = _shoppingCartRepository.GetItem(id);
        //    byte[] buffer = item.Picture;
        //    return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
        //}

    }
}
