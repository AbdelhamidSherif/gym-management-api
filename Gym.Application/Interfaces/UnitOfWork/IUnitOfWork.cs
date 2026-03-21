using System;
using System.Collections.Generic;
using System.Text;
using Gym.Domain.Entities;
using Gym.Application.Interfaces.Repositories;
using System.Formats.Tar;
using Gym.Domain.Common;

namespace Gym.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    IGenericRepository<Member> Members { get; }
    IGenericRepository<Trainer> Trainers { get; }
    IGenericRepository<Session> Sessions { get; }
    IGenericRepository<Booking> Bookings { get; }
    IGenericRepository<MembershipPlan> MembershipPlans { get; }

    //Generic method, when you don't want to add a property foreach entity
    IGenericRepository<TEntity>Repository<TEntity>()where TEntity : BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken ct=default);
}
