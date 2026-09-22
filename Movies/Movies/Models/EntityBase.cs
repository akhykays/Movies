namespace Movies.Models;

public abstract class EntityBase
{
    public Guid Id { get; private init; } = Guid.NewGuid();

    public DateTimeOffset Created { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset Updated { get; private set; } = DateTimeOffset.UtcNow;

    public void UpdateLastModified()
    {
        Updated = DateTimeOffset.UtcNow;
    }
}

