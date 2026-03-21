using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Application.DTOs.Trainers;

public sealed record TrainerListItem(
    int Id,
    string FullName,
    string Speciality
    );