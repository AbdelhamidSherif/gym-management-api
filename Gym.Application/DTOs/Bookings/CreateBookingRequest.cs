using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.Application.DTOs.Bookings;

public sealed record CreateBookingRequest(
    [param: Range(1,int.MaxValue)]int MemberId,
    [param: Range(1,int.MaxValue)]int SessionId
    );