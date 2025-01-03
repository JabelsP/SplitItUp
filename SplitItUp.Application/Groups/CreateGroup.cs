
namespace SplitItUp.Application.Groups;

using MediatR;
using Domain;
using Infrastructure;

public class CreateGroupCommand : IRequest<Group>
{
    public required string GroupName { get; set; }
}

public class CreatePersonCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateGroupCommand, Group>
{
    public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Group(request.GroupName);
        var createdGroup = await dbContext.Groups.AddAsync(group, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return createdGroup.Entity;
    }
}