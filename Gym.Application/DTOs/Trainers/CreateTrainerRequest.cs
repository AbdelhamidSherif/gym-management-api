using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.Application.DTOs.Trainers;

public sealed record CreateTrainerRequest(
    [param: Required, MaxLength(120)] string FullName,
    [param: Required, MaxLength(120)] string Specialty
);