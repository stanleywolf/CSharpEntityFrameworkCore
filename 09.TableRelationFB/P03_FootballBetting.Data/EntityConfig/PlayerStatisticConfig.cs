using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfig
{
    public class PlayerStatisticConfig:IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.Property(x => x.ScoredGoals)
                .HasDefaultValue(0);

            builder.Property(x => x.Assists)
                .HasDefaultValue(0);

            builder.Property(x => x.MinutesPlayed)
                .HasDefaultValue(0);

            builder.HasKey(x => new {x.GameId, x.PlayerId});

            builder.HasOne(x => x.Player)
                .WithMany(x => x.PlayerStatistics)
                .HasForeignKey(x => x.PlayerId);
        }
    }
}
