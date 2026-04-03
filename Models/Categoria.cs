using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFinanceiroCurso.Models
{
    public class Categoria
    {

        public string CategoriaId{ get; set; }


        [Required(ErrorMessage = "O nome da categoria é obrigatório.")] 
        public string Nome { get; set; }
    }
}
