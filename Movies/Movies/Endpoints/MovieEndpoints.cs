using Movies.DTOs;
using Movies.Services;

namespace Movies.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
    {

        var movieApi = routes.MapGroup("/api/movies").WithTags("Movies");

        movieApi.MapGet("/movies", async (IMovieService movieService) =>
        {
            var movies = await movieService.GetAllMoviesAsync();
            return TypedResults.Ok(movies);
        });

        movieApi.MapGet("/{id}", async (IMovieService service, Guid id) =>
        {
            var movie = await service.GetMovieByIdAsync(id);

            return movie is null
                ? (IResult)TypedResults.NotFound(new { Message = $"Movie with ID {id} not found." })
                : TypedResults.Ok(movie);
        });

        movieApi.MapPost("/movies", async (CreateMovieDto createMovieDto, IMovieService movieService) =>
        {
            var movie = await movieService.CreateMovieAsync(createMovieDto);
            return TypedResults.Created($"/movies/{movie.Id}", movie);
        });

        movieApi.MapPut("/movies/{id:guid}", async (Guid id, UpdateMovieDto updateMovieDto, IMovieService movieService) =>
        {
            await movieService.UpdateMovieAsync(id, updateMovieDto);
            return TypedResults.NoContent();
        });

        movieApi.MapDelete("/movies/{id:guid}", async (Guid id, IMovieService movieService) =>
        {
            await movieService.DeleteMovieByIdAsync(id);
            return TypedResults.NoContent();
        });

    }
}

