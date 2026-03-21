using System;
using System.Collections.Generic;
using System.Text;
using Gym.Application.DTOs.Sessions;
using Gym.Application.Services;

namespace Gym.Application.Interfaces.Services;

public interface ISessionService
{
    Task<SessionResponse>CreateAsync(CreateSessionRequest request, CancellationToken ct=default);

    Task<SessionResponse> GetByIdAsync(int id,CancellationToken ct=default);

    Task<IReadOnlyList<SessionListItem>> ListAsync(CancellationToken ct = default);
}
