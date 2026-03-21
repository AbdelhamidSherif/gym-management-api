using System;
using System.Collections.Generic;
using System.Text;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.FullName).IsRequired().HasMaxLength(150);
        builder.Property(t => t.Speciality).IsRequired().HasMaxLength(100);

        builder.HasMany(t => t.Sessions)
               .WithOne(s => s.Trainer)
               .HasForeignKey(s => s.TrainerId);
    }
}
