using Movies.DTOs;

namespace Movies.Services;

public interface IMovieService
{
    Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto);
    Task<MovieDto?> GetMovieByIdAsync(Guid id);
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task UpdateMovieAsync(Guid id, UpdateMovieDto command);
    Task DeleteMovieByIdAsync(Guid id);
}

