using System;
using System.Collections.Generic;
using System.Text;
using Gym.Application.DTOs.Trainers;
using Gym.Application.Services;

namespace Gym.Application.Interfaces.Services;

public interface ITrainerService
{
    Task<TrainerResponse> CreateAsync(CreateTrainerRequest request, CancellationToken ct = default);
    Task<TrainerResponse> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<TrainerListItem>> ListAsync(CancellationToken ct = default);
}