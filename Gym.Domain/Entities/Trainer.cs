using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Gym.Domain.Common;

namespace Gym.Domain.Entities;

public class Trainer : BaseEntity
{
    public string FullName { get; private set; } = null!;

    public string Speciality { get; private set; } = null!;

    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions;

    private Trainer() { }

    public Trainer(string fullName, string speciality)
        => (FullName, Speciality) = (fullName, speciality);
}

