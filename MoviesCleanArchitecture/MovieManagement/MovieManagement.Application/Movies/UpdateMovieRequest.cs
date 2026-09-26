using MovieManagement.Domain.Movies;

namespace MovieManagement.Application.Movies;

public record UpdateMovieRequest(string Title, string Director, DateOnly ReleaseDate, Genre Genre, string Synopsis);

