using Microsoft.EntityFrameworkCore;
using Movies.DTOs;
using Movies.Models;
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

    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
    {
        var movie = Movie.Create(
            createMovieDto.Title,
            createMovieDto.Genre,
            createMovieDto.ReleaseDate,
            createMovieDto.Rating
        );

        await _dbContext.AddAsync(movie);
        await _dbContext.SaveChangesAsync();

        return new MovieDto(
            movie.Id,
            movie.Title,
            movie.Genre,
            movie.ReleaseDate,
            movie.Rating
            );
    }

    public async Task DeleteMovieByIdAsync(Guid id)
    {
        var movie = await _dbContext.Movies
                            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie is not null)
        {
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        return await _dbContext.Movies
                        .AsNoTracking()
            .Select(m => new MovieDto(
                m.Id,
                m.Title,
                m.Genre,
                m.ReleaseDate,
                m.Rating
            ))
            .ToListAsync();
    }

    public async Task<MovieDto?> GetMovieByIdAsync(Guid id)
    {
        var movie = _dbContext.Movies
                        .AsNoTracking()
                        .FirstOrDefault(m => m.Id == id);

        if (movie is null)
        {
            return null;
        }

        return new MovieDto(
            movie.Id,
            movie.Title,
            movie.Genre,
            movie.ReleaseDate,
            movie.Rating
        );
    }

    public async Task UpdateMovieAsync(Guid id, UpdateMovieDto command)
    {
        var movie = await _dbContext.Movies
                            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            throw new NullReferenceException($"Movie with id {id} not found.");
        }

        movie.Update(
            command.Title,
            command.Genre,
            command.ReleaseDate,
            command.Rating);

        _dbContext.Movies.Update(movie);
        await _dbContext.SaveChangesAsync();
    }
}

