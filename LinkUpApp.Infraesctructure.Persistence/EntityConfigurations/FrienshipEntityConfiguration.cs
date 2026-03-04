using LinkUpApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUpApp.Infraesctructure.Persistence.EntityConfigurations
{
    public class FrienshipEntityConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Friendships");
            builder.HasIndex(f => f.SenderUserId);
            builder.HasIndex(f => f.ReceiverUserId);
            #endregion

            #region Property Configuration
            builder.HasIndex(f => new { f.SenderUserId, f.ReceiverUserId }).IsUnique();
            #endregion

            #region Relationships
            #endregion
        }
    }
}
