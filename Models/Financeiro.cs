using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFinanceiroCurso.Models
{
    public class Financeiro
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Digite uma descrição!")] // A anotação [Required] é usada para garantir que o campo "Descricao" seja preenchido, e a mensagem de erro personalizada é fornecida para orientar o usuário caso ele deixe esse campo vazio.
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Digite um valor!")] // A anotação [Required] é usada para garantir que o campo "Valor" seja preenchido, e a mensagem de erro personalizada é fornecida para orientar o usuário caso ele deixe esse campo vazio.
        public double Valor { get; set; }

        [Required(ErrorMessage = "Digite uma data!")] // A anotação [Required] é usada para garantir que o campo "DataOperacao" seja preenchido, e a mensagem de erro personalizada é fornecida para orientar o usuário caso ele deixe esse campo vazio.
        public DateTime DataOperacao { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria!")] // A anotação [Required] é usada para garantir que o campo "CategoriaId" seja preenchido, e a mensagem de erro personalizada é fornecida para orientar o usuário caso ele deixe esse campo vazio.
        public string CategoriaId { get; set; }

        [ValidateNever] // Evita a validação do modelo para esta propriedade, útil para evitar erros de validação ao carregar dados relacionados.
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Selecione uma transação!")] // A anotação [Required] é usada para garantir que o campo "TransacaoId" seja preenchido, e a mensagem de erro personalizada é fornecida para orientar o usuário caso ele deixe esse campo vazio.
        public string TransacaoId { get; set; }

        [ValidateNever]
        public Transacao Transacao { get; set; }


    }
}
