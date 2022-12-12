using AppLivrosDesafio.Data.Models;

namespace AppLivrosDesafio.ViewModels
{
    // Criação do model para poder visualizar informações dos livros - 09/12 - YAGO
    public class BookViewModel 
    {
        public Book Book { get; set; }        
        public IEnumerable<User>  Users { get; set; }
    }
}
