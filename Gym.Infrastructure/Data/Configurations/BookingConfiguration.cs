using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BookingDate).IsRequired();
        builder.HasIndex(x => new { x.MemberId, x.SessionId }).IsUnique().HasDatabaseName("UX_Bookings_Member_Session");
        builder.HasOne(x => x.Member).WithMany(x => x.Bookings).HasForeignKey(x => x.MemberId);
        builder.HasOne(x => x.Session).WithMany(x => x.Bookings).HasForeignKey(x => x.SessionId);
    }
}
