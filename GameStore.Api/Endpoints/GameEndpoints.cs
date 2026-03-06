using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
    new(
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992,7,15)
    ),
     new(
        2,
        "Final Fanstay VII Rebrith",
        "RPG",
        69.99M,
        new DateOnly(2024,2,29)
    ),
     new(
        3,
        "Astro Bot",
        "Platformer",
        59.99M,
        new DateOnly(2024,9,6)
    )
];

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        //Get /games
        group.MapGet("/", () => games);


        //Get /games/id
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        //Create /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
           Game game = new()
           {
             Name= newGame.Name,
             GenreId = newGame.GenreId,
             Price= newGame.Price,
             ReleaseDate= newGame.ReleaseDate  
           };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

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
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index <= 0)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
             id,
             updateGame.Name,
             updateGame.Genre,
             updateGame.Price,
             updateGame.ReleaseDate
            );

            return Results.NoContent();
        });
    }
}
