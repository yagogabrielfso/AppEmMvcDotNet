using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLivrosDesafio.Data.Models
{

    //Criando o model de livros para poder criar a tabela no banco de dados a partir do migrations - 08/12 - YAGO

    public class Book
    {
        // Campo chave da tabela
        [Key]
        public int? BookId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }
        [MaxLength(50)]

        // Foreign key   
        [Display(Name = "User")]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }       

        public string? ReleaseDate { get; set; }

        //Adicionando novas propriedades para migrations - 09-12 - YAGO
        [Required]
        public string? Author { get; set; }
        
        [MaxLength(250)]
        public string? Synopsis { get; set; }

        public string? Publisher { get; set; }

        // Falta gerar o codigo isbn automaticamente, - 09-12 - YAGO
        [StringLength(13, MinimumLength = 13, ErrorMessage = "The ISBN must be a string with the exact length of 13.")]        
        public long? ISBN { get; set; }
                
        //Adicionar imagem da capa do livro - 09-12 - YAGO        
        // Comentando por falta de tempo para implementação
        //public byte[] Image { get; set; }
        



    }
}
