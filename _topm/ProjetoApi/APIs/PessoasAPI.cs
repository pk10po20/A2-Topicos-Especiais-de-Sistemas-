using Microsoft.EntityFrameworkCore;

public static class PessoasApi
{

    public static void MapPessoasApi(this WebApplication app)
    {

        var group = app.MapGroup("/pessoas");

        group.MapGet("/", async (BancoDeDados db) => await db.Pessoas.ToListAsync()); //select * from pessoas

        group.MapPost("/", async (BancoDeDados db, Pessoa pessoa) =>
        {
            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();
            return Results.Created($"/pessoas/{pessoa.Id}", pessoa);
        });

        group.MapPut("/{id}", async (BancoDeDados db, int id, Pessoa pessoaAlterada) =>
        {
            //select * from pessoas where id = id
            var pessoa = await db.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return Results.NotFound();
            }

            pessoa.Nome = pessoaAlterada.Nome;
            pessoa.CPF = pessoaAlterada.CPF;
            pessoa.Telefone = pessoaAlterada.Telefone;
            pessoa.Email = pessoaAlterada.Email;

            //update pessoas set nome = 'pessoaAlterada.Nome', cpf = 'pessoaAlterada.CPF', telefone = 'pessoaAlterada.Telefone', email = 'pessoaAlterada.Email' where id = id
            await db.SaveChangesAsync();
            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (BancoDeDados db, int id) =>
        {
            //select * from pessoas where id = id
            var pessoa = await db.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return Results.NotFound();
            }

            //delete from pessoas where id = id
            db.Pessoas.Remove(pessoa);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

    }


}