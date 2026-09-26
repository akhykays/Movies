using MovieManagement.Domain.Movies;

namespace MovieManagement.Application.Movies;

public record CreateMovieRequest(string Title, string Director, DateOnly ReleaseDate, Genre Genre, string Synopsis);
