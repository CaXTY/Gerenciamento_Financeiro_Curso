using GerenciamentoFinanceiroCurso.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFinanceiroCurso.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // As propriedades DbSet representam as tabelas do banco de dados para as entidades Categoria, Transacao e Financeiro. Elas permitem que o Entity Framework Core realize operações de CRUD (Create, Read, Update, Delete) nessas tabelas.
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Financeiro> Financas { get; set; }


        // O método OnModelCreating é usado para configurar o modelo de dados e as relações entre as entidades. Ele é chamado quando o modelo é criado e pode ser usado para definir chaves primárias, relacionamentos, restrições, etc. No exemplo fornecido, o método está vazio, mas você pode adicionar configurações específicas para suas entidades conforme necessário.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // O método HasData é usado para inserir dados iniciais (seed data) nas tabelas do banco de dados. No exemplo fornecido, ele está sendo usado para adicionar categorias e transações pré-definidas. Isso é útil para garantir que o banco de dados tenha dados básicos para trabalhar, especialmente durante o desenvolvimento ou para fins de teste.
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { CategoriaId = "educacao", Nome = "Educação" },
                new Categoria { CategoriaId = "salario", Nome = "Salário" },
                new Categoria { CategoriaId = "viagem", Nome = "Viagem" },
                new Categoria { CategoriaId = "mercado", Nome = "Mercado" },
                new Categoria { CategoriaId = "comissao", Nome = "Comissão" }
            );


            modelBuilder.Entity<Transacao>().HasData(
                
                new Transacao { TransacaoId = "ganho", Nome = "Ganho" },
                new Transacao { TransacaoId = "gasto", Nome = "Gasto" }
                );


            base.OnModelCreating(modelBuilder);
        }


    }
}
