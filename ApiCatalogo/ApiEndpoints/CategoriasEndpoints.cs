using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.ApiEndpoints
{
    public static class CategoriasEndpoints
    {
        public static  void  MapCategoriasEndpoints(this WebApplication app)
        {
            //Categorias
            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).RequireAuthorization();

            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();
                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            });

            app.MapGet("/categorias{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id)
                        is Categoria categoria ? Results.Ok(categoria) : Results.NotFound("Categoria não econtrada");
            });

            app.MapPut("/categorias{id}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.CategoriaId != id)
                    return Results.BadRequest();

                var categoriaDB = await db.Categorias.FindAsync(id);

                if (categoriaDB is null)
                    return Results.NotFound();

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.ImagemUrl = categoria.ImagemUrl;

                await db.SaveChangesAsync();
                return Results.NoContent();

            });

            app.MapDelete("/categorias{id}", async (int id, AppDbContext db) =>
            {
                if (await db.Categorias.FindAsync(id) is Categoria categoria)
                {
                    db.Categorias.Remove(categoria);
                    await db.SaveChangesAsync();
                    return Results.Ok(categoria);
                }
                return Results.NotFound();

            });
        }
    }
}
