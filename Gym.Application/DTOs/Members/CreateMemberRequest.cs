using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Gym.Application.DTOs.Members;

public sealed record CreateMemberRequest(
    [param: Required, MaxLength(120)] string FullName,
    [param: Required, RegularExpression(@"^(01[0125][0-9]{8}|\+201[0125][0-9]{8})$", ErrorMessage = "Phone must be a valid Egyptian mobile number (010xxxxxxxx or +201xxxxxxxx)"), StringLength(14, MinimumLength = 11)] string Phone,
    [Required,EmailAddress,MaxLength(200)]string Email,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    [param: Range(1,int.MaxValue)]int MembershipPlanId
):IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MembershipEndDate <= MembershipStartDate)
        {
            //yield return new ValidationResult("Membership end date must be after the start date.", new[] { nameof(MembershipEndDate) });

            yield return new ValidationResult(
                "MembershipEndDate must be greater than to MembershipStartDate.",
                [nameof(MembershipStartDate), nameof(MembershipEndDate)]);
        }
    }
}