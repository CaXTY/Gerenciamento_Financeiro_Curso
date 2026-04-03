using GerenciamentoFinanceiroCurso.Data;
using GerenciamentoFinanceiroCurso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFinanceiroCurso.Controllers
{
    public class HomeController : Controller
    {
        // Injeção de dependência do contexto do banco de dados
        private readonly AppDbContext _context;

        // Construtor para receber o contexto do banco de dados
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index(string id)
        {
            // Criar uma instância de Filtros usando o ID fornecido
            var filtros = new Filtros(id);

            // Passar os filtros para a View usando ViewBag
            ViewBag.Filtros = filtros;
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Transacoes = _context.Transacoes.ToList();
            ViewBag.DataOperacao = Filtros.ValoresDataOperacao;

            // Construir a consulta usando os filtros
            IQueryable<Financeiro> consulta = _context.Financas
                                               .Include(x => x.Transacao)
                                               .Include(x => x.Categoria);


            // Aplicar os filtros à consulta
            if (filtros.TemCategoria)
            {
                consulta = consulta.Where(c => c.CategoriaId == filtros.CategoriaId);
            }

            if (filtros.TemTransacao)
            {
                consulta = consulta.Where(c => c.TransacaoId == filtros.TransacaoId);
            }
    

            // Filtro para data de operação
            if (filtros.TemDataOperacao)
            {
                var hoje = DateTime.Today;

                if (filtros.EhPapassado)
                {
                    consulta = consulta.Where(c => c.DataOperacao < hoje);

                }
                if (filtros.EhFuturo)
                {
                    consulta = consulta.Where(c => c.DataOperacao > hoje);
                }

                if (filtros.EhHoje)
                {
                    consulta = consulta.Where(c => c.DataOperacao == hoje);
                }
            }

            // Ordenar os resultados por data de operação
            var financas = consulta.OrderBy(d => d.DataOperacao).ToList();

            // Retornar a View com os resultados filtrados
            return View(financas);
        }

        // GET: Home/AdicionarTransacao
        public IActionResult AdicionarTransacao()
        {

            // Passar as categorias e transações para a View usando ViewBag
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Transacoes = _context.Transacoes.ToList();

            return View();
        }


        // POST: Home/RemoverTransacao
        public IActionResult RemoverTransacao(int id)
        {
            // Encontrar a transação pelo ID
            var financa = _context.Financas.Find(id);

            // Verificar se a transação existe
            if (financa != null)
            {
                _context.Remove(financa);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // GET: Home/AdicionarCategoria
        public IActionResult AdicionarCategoria()
        {
            // Criar uma nova instância de Categoria para passar para a View
            var categoria = new Categoria { CategoriaId = "categoria" };

            return View(categoria);
        }


        // GET: Home/SomatoriaValores
        public IActionResult SomatoriaValores()
        {
            // Realizar a consulta para agrupar os dados por categoria e calcular os totais
            var resultados = from g in _context.Financas
                                        .Include(x => x.Categoria)
                                        .Include(x => x.Transacao)
                                        .ToList()
                             // Agrupar os dados por categoria usando o método GroupBy, onde a chave de agrupamento é a CategoriaId
                             group g by new { g.CategoriaId } into total
                             // Criar um novo objeto anônimo para cada grupo, contendo o nome da categoria, o nome da transação, a data da operação e o total calculado
                             select new
                             {
                                 CategoriaNome = total.First().Categoria.Nome,
                                 TransacaoNome = total.First().Transacao.Nome,
                                 DataOperacao = total.First().DataOperacao,
                                 Total = total.Sum(c => c.Valor)

                             };

            // Calcular os ganhos, gastos e a diferença total
            var ganhos = _context.Financas
                            .Include(x => x.Categoria)
                            .Include(x => x.Transacao)
                            .Where(x => x.TransacaoId == "ganho")
                            .Sum(x => x.Valor);

            // Calcular os gastos
            var gastos = _context.Financas
                            .Include(x => x.Categoria)
                            .Include(x => x.Transacao)
                            .Where(x => x.TransacaoId == "gasto")
                            .Sum(x => x.Valor);


            // Calcular a diferença entre ganhos e gastos
            var diferenca = ganhos - gastos;


            // Criar uma lista de RegistrosFinanceiros para passar para a View
            List<RegistrosFinanceiros> registros = new List<RegistrosFinanceiros>();


            // Preencher a lista de RegistrosFinanceiros com os resultados da consulta
            foreach (var resultado in resultados)
            {
                // Criar um novo registro financeiro para cada resultado da consulta
                var registro = new RegistrosFinanceiros()
                {
                    CategoriaNome = resultado.CategoriaNome,
                    TransacaoNome = resultado.TransacaoNome,
                    DataOperacao = resultado.DataOperacao.ToString("dd/MM/yyyy"),
                    ValorCategoria = (decimal)resultado.Total,
                    Ganhos = (decimal)ganhos,
                    Gastos = (decimal)gastos,
                    Diferenca = (decimal)diferenca,

                };

                registros.Add(registro);
            }


            return View(registros);

        }

        // POST: Home/Filtrar
        [HttpPost]
        public IActionResult Filtrar(string[] filtro)
        {
            string id = string.Join("-", filtro);
            return RedirectToAction("Index", new { ID = id });
        }

        // POST: Home/AdicionarCategoria
        [HttpPost]
        public IActionResult AdicionarCategoria(Categoria categoria)
        {
            // Verificar se o modelo é válido antes de adicionar a categoria ao banco de dados
            if (ModelState.IsValid)
            {

                var categoriaBanco = new Categoria
                {
                    CategoriaId = categoria.Nome.ToLower(),
                    Nome = categoria.Nome,
                };

                // Adicionar a nova categoria ao banco de dados e salvar as alterações
                _context.Categorias.Add(categoriaBanco);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            // Se o modelo não for válido, recarregar as categorias para a View
            else
            {
                return View(categoria);
            }
        }

        // POST: Home/AdicionarTransacao
        [HttpPost]
        public IActionResult AdicionarTransacao(Financeiro financeiro)
        {

            // Verificar se o modelo é válido antes
            if (ModelState.IsValid)
            {
                _context.Financas.Add(financeiro);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // Se o modelo não for válido, recarregar as categorias e transações para a View
            else
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                ViewBag.Transacoes = _context.Transacoes.ToList();

                return View(financeiro);

            }
        }
    }
}