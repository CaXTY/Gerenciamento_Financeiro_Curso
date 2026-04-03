namespace GerenciamentoFinanceiroCurso.Models
{
    public class RegistrosFinanceiros
    {
        // Propriedades para exibir os dados na View
        public string CategoriaNome { get; set; }
        public string TransacaoNome { get; set; }
        public string DataOperacao { get; set; }
        public decimal Ganhos { get; set; }
        public decimal Gastos { get; set; }
        public decimal ValorCategoria { get; set; }
        public decimal Diferenca { get; set; }
    }
}
