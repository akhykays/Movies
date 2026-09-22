using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Models;

namespace Movies.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(m => m.Genre)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(m => m.ReleaseDate)
               .IsRequired();

        builder.Property(m => m.Rating)
               .IsRequired();

        builder.Property(m => m.Created)
               .IsRequired()
               .ValueGeneratedOnAdd();

        builder.Property(m => m.Updated)
               .IsRequired()
               .ValueGeneratedOnUpdate();

        builder.HasQueryFilter("Released", m => m.ReleaseDate <= DateTimeOffset.UtcNow);

        // Named query filter: only show movies rated 7 or higher
        builder.HasQueryFilter("HighRated", m => m.Rating >= 7);

        builder.HasIndex(m => m.Title);
    }
}

