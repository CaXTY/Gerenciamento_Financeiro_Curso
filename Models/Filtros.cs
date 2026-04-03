namespace GerenciamentoFinanceiroCurso.Models
{
    public class Filtros
    {
        // Construtor que recebe a string de filtros e a processa
        public Filtros(string filtrostring) 
        {
            // Se a string de filtros for nula ou vazia, usamos um valor padrão
            FiltroString = filtrostring ?? "todos-todos-todos";
            string[] filtros = FiltroString.Split('-');

            // Validar e atribuir os valores dos filtros às propriedades correspondentes
            CategoriaId = filtros.Length > 0 ? filtros[0] : "todos";
            DataOperacao = filtros.Length > 1 ? filtros[1] : "todos";
            TransacaoId = filtros.Length > 2 ? filtros[2] : "todos";
        }



        // Propriedades para os filtros
        public string FiltroString { get; set; }

        public string CategoriaId { get; set; }

        public string TransacaoId { get; set; }

        public string DataOperacao { get; set; }


        // Propriedades para verificar se os filtros estão ativos
        public bool TemCategoria => CategoriaId.ToLower() != "todos";
        public bool TemTransacao => TransacaoId.ToLower() != "todos";
        public bool TemDataOperacao => DataOperacao.ToLower() != "todos";

        // Dicionário para mapear os valores de data de operação para exibição
        public static Dictionary<string, string> ValoresDataOperacao =>
            new Dictionary<string, string>
            {
                {"passado", "Passado" },
                {"futuro", "Futuro" },
                {"hoje", "Hoje" }
            };


        // Propriedades para verificar o valor do filtro de data de operação
        public bool EhPapassado => DataOperacao.ToLower() == "passado";

        public bool EhFuturo => DataOperacao.ToLower() == "futuro";

        public bool EhHoje => DataOperacao.ToLower() == "hoje";
    }

}
