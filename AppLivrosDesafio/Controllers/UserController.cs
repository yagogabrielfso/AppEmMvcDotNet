using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppLivrosDesafio.Data.Interfaces;
using AppLivrosDesafio.Data.Models;
using AppLivrosDesafio.ViewModels;



namespace AppLivrosDesafio.Controllers
{
    /************************************ // 
    O Controller recebe as requisições das views e é direcionado para a "route" com a action especifica a partir da VIEW - 08/12 - YAGO
    *************************************/

    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        [Route("User")]
        public IActionResult List()
        {
            if (!_repository.Any()) return View("Empty");

            var users = _repository.GetAllWithBooks();

            return View(users);
        }

        public IActionResult UserDetail()
        {
            var users = _repository.GetAllWithBooks();

            if (users?.ToList().Count == 0)
            {
                return View("Empty");
            }

            return View(users);
        }

        public IActionResult Detail(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Update(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _repository.Update(user);

            return RedirectToAction("List");
        }

        public ViewResult Create()
        {
            return View(new CreateUserViewModel { Referer = Request.Headers["Referer"].ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel userVM)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(userVM);
            //}
            try
            {
                _repository.Create(userVM.User);
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has ocurred");
            }           

            //if (!String.IsNullOrEmpty(userVM.Referer))
            //{
            //    return Redirect(userVM.Referer);
            //}

           
        }

        public IActionResult Delete(int id)
        {
            var user = _repository.GetById(id);

            _repository.Delete(user);

            return RedirectToAction("List");
        }

    }
}
