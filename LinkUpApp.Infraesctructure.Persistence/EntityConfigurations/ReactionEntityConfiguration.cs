using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class ReactionEntityConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Reactions");
            #endregion

            #region Property Configuration
            #endregion

            #region Relationships
            #endregion
        }
    }
}
