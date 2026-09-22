using Movies.DTOs;
using Movies.Persistance;

namespace Movies.Services;

public class MovieService : IMovieService
{
    private readonly MovieDbContext _dbContext;
    private readonly ILogger<MovieService> _logger;

    public MovieService(MovieDbContext dbContext, ILogger<MovieService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMovieByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MovieDto?> GetMovieByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMovieAsync(Guid id, UpdateMovieDto command)
    {
        throw new NotImplementedException();
    }
}

