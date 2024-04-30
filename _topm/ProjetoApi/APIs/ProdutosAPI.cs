using Microsoft.EntityFrameworkCore;

public static class ProdutosApi
{

    public static void MapProdutosApi(this WebApplication app)
    {

        var group = app.MapGroup("/Produtos");

        group.MapGet("/", async (BancoDeDados db) => await db.Produtos.ToListAsync()); //select * from Produtos

        group.MapPost("/", async (BancoDeDados db, Produto produto) =>
        {
            db.Produtos.Add(produto);
            await db.SaveChangesAsync();
            return Results.Created($"/Produtos/{produto.Id}", produto);
        });

        group.MapPut("/{id}", async (BancoDeDados db, int id, Produto produtoAlterada) =>
        {
            //select * from Produtos where id = id
            var produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return Results.NotFound();
            }

            produto.Nome = produtoAlterada.Nome;
            produto.Descricao = produtoAlterada.Descricao;

            //update Produtos set nome = 'produtoAlterada.Nome', cpf = 'produtoAlterada.CPF', telefone = 'produtoAlterada.Telefone', email = 'produtoAlterada.Email' where id = id
            await db.SaveChangesAsync();
            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (BancoDeDados db, int id) =>
        {
            //select * from Produtos where id = id
            var produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return Results.NotFound();
            }

            //delete from Produtos where id = id
            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

    }


}