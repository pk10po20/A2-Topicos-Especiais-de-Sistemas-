using Microsoft.EntityFrameworkCore;

public static class VendaApi
{

    public static void MapVendaApi(this WebApplication app)
    {

        var group = app.MapGroup("/Venda");

        group.MapGet("/", async (BancoDeDados db) => await db.Venda.ToListAsync()); //select * from Venda

        group.MapPost("/", async (BancoDeDados db, Venda venda) =>
        {
            db.Venda.Add(venda);
            await db.SaveChangesAsync();
            return Results.Created($"/Venda/{venda.Id}", venda);
        });

        group.MapPut("/{id}", async (BancoDeDados db, int id, Venda vendaAlterada) =>
        {
            //select * from Venda where id = id
            var venda = await db.Venda.FindAsync(id);
            if (venda == null)
            {
                return Results.NotFound();
            }

            venda.Valor = vendaAlterada.Valor;
            venda.Nome = vendaAlterada.Nome;
            venda.Descricao = vendaAlterada.Descricao;

            //update Venda set nome = 'vendaAlterada.Nome', cpf = 'vendaAlterada.CPF', telefone = 'vendaAlterada.Telefone', email = 'vendaAlterada.Email' where id = id
            await db.SaveChangesAsync();
            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (BancoDeDados db, int id) =>
        {
            //select * from Venda where id = id
            var venda = await db.Venda.FindAsync(id);
            if (venda == null)
            {
                return Results.NotFound();
            }

            //delete from Venda where id = id
            db.Venda.Remove(venda);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

    }


}