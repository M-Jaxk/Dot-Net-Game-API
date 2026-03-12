using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        //Get /games
        group.MapGet("/", async(GameStoreContext dbContext) => 
        await dbContext.Games
                       .Include(game => game.Genre)
                       .Select(game=> new GameSummaryDto(
                        game.Id,
                        game.Name,
                        game.Genre!.Name,
                        game.Price,
                        game.ReleaseDate
                       ))
                       .AsNoTracking()
                       .ToListAsync());


        //Get /games/id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                )
            );
        }).WithName(GetGameEndpointName);

        //Create /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
           Game game = new()
           {
             Name= newGame.Name,
             GenreId = newGame.GenreId,
             Price= newGame.Price,
             ReleaseDate= newGame.ReleaseDate  
           };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDTO = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute("GetGame", new { id = gameDTO.Id }, gameDTO);
        });

        //Update /games/id
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            
            existingGame.Name = updateGame.Name;
            existingGame.GenreId = updateGame.GenreId;
            existingGame.Price = updateGame.Price;
            existingGame.ReleaseDate = updateGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}",async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                           .Where(game=> game.Id == id)
                           .ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}
