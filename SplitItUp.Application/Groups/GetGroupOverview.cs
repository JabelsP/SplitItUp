using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SplitItUp.Application.Groups.DTOs;
using SplitItUp.Infrastructure;

namespace SplitItUp.Application.Groups;

public class GetGroupOverviewCommand : IRequest<GroupOverviewDto>
{
    public required Guid GroupId { get; set; }
}

public class GetGroupOverviewCommandHandler(AppDbContext dbContext)
    : IRequestHandler<GetGroupOverviewCommand, GroupOverviewDto>
{
    public async Task<GroupOverviewDto> Handle(GetGroupOverviewCommand request, CancellationToken cancellationToken)
    {
        return await dbContext.Groups
            .Include(x => x.Members)
            .Where(x => x.Id == request.GroupId)
            .Select(x => new GroupOverviewDto
            {
                Id = x.Id,
                Name = x.Name,
                Members = x.Members.Select(p => new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList()
            }).FirstAsync(cancellationToken);
    }
}