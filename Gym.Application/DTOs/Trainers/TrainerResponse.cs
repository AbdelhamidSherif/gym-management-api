using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Application.DTOs.Trainers;

public sealed record TrainerResponse(
    int Id,
    string FullName,
    string Specialty,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
