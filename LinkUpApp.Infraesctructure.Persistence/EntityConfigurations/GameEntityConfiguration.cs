using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Games");
            #endregion

            #region Property Configuration
            #endregion

            #region Relationships
            builder.HasMany<Shot>(g => g.Shots)
                .WithOne(s => s.Game)
                .HasForeignKey(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ShipPosition>(g => g.Ships)
                .WithOne(sp => sp.Game)
                .HasForeignKey(sp => sp.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
