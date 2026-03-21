using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Application.DTOs.Members;

public sealed record MemberListItem(
    int Id,
    string FullName,
    string Phone,
    string Email,
    string Status
);