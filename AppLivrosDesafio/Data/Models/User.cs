using AppLivrosDesafio.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLivrosDesafio.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserId { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }

        public ICollection<Book> Books { get; set; }



    }
}