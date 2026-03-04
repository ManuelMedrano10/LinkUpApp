using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Posts");
            #endregion

            #region Property Configuration
            builder.Property(p => p.Content).HasMaxLength(255);
            #endregion

            #region Relationships
            builder.HasMany<Comment>(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Reaction>(p => p.Reactions)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
