using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class ShipPositionEntityConfiguration : IEntityTypeConfiguration<ShipPosition>
    {
        public void Configure(EntityTypeBuilder<ShipPosition> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Ships");
            #endregion

            #region Property Configuration
            #endregion

            #region Relationships
            #endregion
        }
    }
}
