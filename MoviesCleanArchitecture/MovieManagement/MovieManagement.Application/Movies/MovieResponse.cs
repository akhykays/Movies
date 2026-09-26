using MovieManagement.Domain.Movies;

namespace MovieManagement.Application.Movies;

public record MovieResponse(Guid Id, string Title, string Director, DateOnly ReleaseDate, string Genre, string Synopsis, double? AverageRating, int RatingCount);
