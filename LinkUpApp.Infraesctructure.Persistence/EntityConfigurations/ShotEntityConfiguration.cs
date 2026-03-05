using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class ShotEntityConfiguration : IEntityTypeConfiguration<Shot>
    {
        public void Configure(EntityTypeBuilder<Shot> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Shots");
            #endregion

            #region Property Configuration
            #endregion

            #region Relationships
            #endregion
        }
    }
}
