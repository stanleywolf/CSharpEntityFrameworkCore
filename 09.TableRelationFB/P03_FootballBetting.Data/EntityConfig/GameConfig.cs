using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfig
{
    public class GameConfig:IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Result)
                .IsRequired();
            builder.Property(x => x.HomeTeamGoals)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(x => x.AwayTeamGoals)
                .IsRequired()
                .HasDefaultValue(0);

            //builder.HasMany(x => x.Bets)
            //    .WithOne(x => x.Game)
            //    .HasForeignKey(x => x.GameId);
        }
    }
}
