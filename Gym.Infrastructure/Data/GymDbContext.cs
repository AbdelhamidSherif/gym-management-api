using System;
using System.Collections.Generic;
using System.Text;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Data;

public class GymDbContext : DbContext
{

    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }


    public DbSet<Member> Members => Set<Member>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Booking> bookings => Set<Booking>();
    public DbSet<MembershipPlan>  membershipPlans=> Set<MembershipPlan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
