using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class ReplyEntityConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Replies");
            #endregion

            #region Property Configuration
            builder.Property(rp => rp.Content).HasMaxLength(255);
            #endregion

            #region Relationships
            #endregion
        }
    }
}
