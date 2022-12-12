using Microsoft.AspNetCore.Identity;

namespace AppLivrosDesafio.Data.Models
{
    public class AppLivrosUser : IdentityUser
    {
        // Criando a propriedade Name para acrescentar no banco de dados a partir do migrations - 08/12 - YAGO
        public string? Name { get; set; }
    }
}
