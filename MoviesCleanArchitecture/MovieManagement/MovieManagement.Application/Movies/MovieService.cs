using Microsoft.EntityFrameworkCore;
using MovieManagement.Application.Common;
using MovieManagement.Domain.Movies;

namespace MovieManagement.Application.Movies;

// AsNoTracking skips change-tracking on read-only queries, which cuts
// allocations and runs faster. Use it on every query that only reads.
public sealed class MovieService : IMovieService
{
    private IApplicationDbContext _dbContext;
    public MovieService(IApplicationDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        _dbContext = dbContext;
    }
    public async Task<MovieResponse> CreateAsync(CreateMovieRequest request, CancellationToken cancellationToken)
    {
        var movie = Movie.Create(request.Title, request.Director, request.ReleaseDate, request.Genre, request.Synopsis);

        _dbContext.Movies.Add(movie);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return movie.ToResponse();
    }

    public async Task<MovieResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        return movie?.ToResponse();
    }

    public async Task<IReadOnlyList<MovieResponse>> GetAllAsync(CancellationToken cancellationToken)
    {

        var movies = await _dbContext.Movies
            .AsNoTracking()
            .OrderByDescending(m => m.ReleaseDate)
            .ToListAsync(cancellationToken);

        return movies.Select(m => m.ToResponse()).ToList();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateMovieRequest request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (movie is null)
        {
            return false;
        }

        movie.UpdateDetails(request.Title, request.Director, request.ReleaseDate, request.Genre, request.Synopsis);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> AddRatingAsync(Guid id, AddRatingRequest request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (movie is null)
        {
            return false;
        }

        // The Movie checks the score and updates its own average. A bad score
        // throws, and the API turns that into a 400.
        movie.AddRating(request.Score);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (movie is null)
        {
            return false;
        }

        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
