using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.Application.DTOs.MembershipPlans;

public sealed record UpdateMembershipPlanRequest(
    [param: Required, MaxLength(120)] string Name,
    [param: Range(typeof(decimal), "0.01","1000000")]decimal Price,
    [param: Range(1,200)]int MaxSessionsPerMonth
    );