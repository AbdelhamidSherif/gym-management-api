using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.Application.DTOs.Members;

public sealed record UpdateMemberRequest(
[param: Required, MaxLength(120)] string FullName,
//[param: Required, Phone, MaxLength(25)] string Phone,
[param: Required, RegularExpression(
    @"^(01[0125][0-9]{8}|\+201[0125][0-9]{8})$",
    ErrorMessage ="Phone must be valid Egyptian mobile number (010xxxxxxxx) or (+201xxxxxxxx"),
    StringLength(14,MinimumLength =11)]string  Phone,
[param: Required, EmailAddress, MaxLength(200)] string Email,
DateOnly MembershipStartDate,
DateOnly MembershipEndDate,
[param: Range(1, int.MaxValue)] int MembershipPlanId
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MembershipEndDate <= MembershipStartDate)
        {
            yield return new ValidationResult(
                "MembershipEndDate must be greater than to MembershipStartDate.",
                [nameof(MembershipStartDate), nameof(MembershipEndDate)]);
        }
    }
}
