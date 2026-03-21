using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using Gym.Application.DTOs.Common;
using Gym.Application.DTOs.Members;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;


namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    private readonly IUnitOfWork _uow;

    public MemberService(IUnitOfWork uow) => _uow = uow;

    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {

        MembershipPlan plan = await GetMembershipPlanAsync(request.MembershipPlanId, ct);

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        await EnsureEmailIsUniqueAsynce(request, normalizedEmail, ct);

        var member = new Member(
            fullName: request.FullName,
            phone: request.Phone,
            email: normalizedEmail,
            startDate: request.MembershipStartDate,
            endDate: request.MembershipEndDate,
            membershipPlanId: request.MembershipPlanId
        );

        await _uow.Members.AddAsync(member, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToResponse(member, planName: plan.Name);
    }

    private async Task<MembershipPlan?> GetMembershipPlanAsync(int membershipPlanId, CancellationToken ct)
    {
        var plan = await _uow.MembershipPlans.GetByIdAsync(membershipPlanId, ct);

        if (plan is null) throw new NotFoundException($"Membership plan with id {membershipPlanId} was not found.");
        return plan;
    }

    private async Task EnsureEmailIsUniqueAsynce(CreateMemberRequest request, string emailNormalized, CancellationToken ct)
    {
        var allMembers = await _uow.Members.ListAsync(ct);

        var isExist = allMembers.Any(e => e.Email is not null && e.Email == emailNormalized);

        if (isExist) throw new ConflictException($"Email '{request.Email}' is already used.");
    }

    public async Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var member = await _uow.Members.GetByIdAsync(id, ct);

        if (member is null) throw new NotFoundException($"Member with id {id} was not found");

        var plan = await _uow.MembershipPlans.GetByIdAsync(member.MembershipPlanId, ct);

        string planName = plan?.Name ?? string.Empty;

        return MapToResponse(member, planName);
    }

    public async Task<IReadOnlyList<MemberListItem>> ListAsync(CancellationToken ct = default)
    {
        var members = await _uow.Members.ListAsync(ct);

        return members
            .Select(m => new MemberListItem(
                Id: m.Id,
                FullName: m.FullName,
                Phone: m.Phone,
                Email: m.Email,
                Status: m.Status.ToString()
                ))
            .ToList();
    }

    private static MemberResponse MapToResponse(Member member, string planName)
        => new(
            Id: member.Id,
            FullName: member.FullName,
            Phone: member.Phone,
            Email: member.Email,
            MembershipStartDate: member.MembershipStartDate,
            MembershipEndDate: member.MembershipEndDate,
            MembershipPlanId: member.MembershipPlanId,
            MembershipPlanName: planName,
            Status: member.Status.ToString(),
            CreatedAt: member.CreatedAt,
            UpdatedAt: member.UpdatedAt
        );

    public async Task<PagedResponse<MemberListItem>> ListPagedAsync(PagedRequest request, CancellationToken ct)
    {
        var term = request.Search?.Trim();

        Expression<Func<Member, bool>>? predicate = null;

        if (!string.IsNullOrWhiteSpace(term))
        {
            predicate = m => m.FullName.Contains(term) ||
            m.Email.Contains(term) ||
            m.Phone.Contains(term);
        }

        var orderBy = BuildMemberOrderBy(request);

        var (members, totalCount) = await _uow.Members.GetPagedAsync(
            predicate,
            orderBy,
            request.Page,
            request.PageSize,
            ct);

        var items = members.Select(m => new MemberListItem(
            m.Id,
            m.FullName,
            m.Phone,
            m.Email,
            Status: m.Status.ToString()
        )).ToList();

        return new PagedResponse<MemberListItem>(items, request.Page, request.PageSize, totalCount);
    }

    private static Func<IQueryable<Member>, IOrderedQueryable<Member>> BuildMemberOrderBy(PagedRequest request)
    {
        var sortBy = request.SortBy?.Trim();
        var desc = request.SortDir == SortDirection.Desc;

        if (string.IsNullOrWhiteSpace(sortBy))
            return q => desc ? q.OrderByDescending(x => x.Id) : q.OrderBy(x => x.Id);

        return sortBy.ToLowerInvariant() switch
        {
            "id" => q => desc ? q.OrderByDescending(x => x.Id) : q.OrderBy(x => x.Id),
            "fullname" => q => desc
            ? q.OrderByDescending(x => x.FullName)
            : q.OrderBy(x => x.FullName),

            _ => q => desc ? q.OrderByDescending(x => x.Id) : q.OrderBy(x => x.Id)
        };
    }
}

