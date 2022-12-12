using AppLivrosDesafio.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppLivrosDesafio.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public User? User { get; set; }
        public string? Referer { get; set; }
    }
}
