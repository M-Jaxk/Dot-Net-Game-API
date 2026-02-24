namespace GameStore.Api.Dtos;

/*
    A Dto is a contract between the client and server. It represents
    a shared aggrement about how data will be transfered and used.
*/
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
