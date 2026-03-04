using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Comments");
            #endregion

            #region Property Configuration
            builder.Property(c => c.Content).HasMaxLength(255);
            #endregion

            #region Relationships
            builder.HasMany<Reply>(C => C.Replies)
                .WithOne(rp => rp.Comment)
                .HasForeignKey(rp => rp.CommetId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
