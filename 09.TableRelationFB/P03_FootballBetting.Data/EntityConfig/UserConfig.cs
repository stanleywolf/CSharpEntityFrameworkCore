using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Password)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Name)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Email)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Balance)
                .IsRequired()
                .IsUnicode();

        }
    }
}
