using Microsoft.EntityFrameworkCore;

public class BancoDeDados : DbContext
{
    //Configuração da conexão
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL("server=localhost;port=3306;database=DesafioCsharp;user=root;password=positivo");
    }

    //Mapeamento das tabelas

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Venda> Venda { get; set; }
    

    //comandos dotnet
    //dotnet ef migrations add CriarTabelaProdutos
    //dotnet ef database update
}