using MediatR;
using Microsoft.EntityFrameworkCore;
using SplitItUp.Domain;
using SplitItUp.Infrastructure;

namespace SplitItUp.Application.Groups;

public class AddPersonCommand : IRequest
{
    public required Guid GroupId { get; set; }
    public required Guid PersonId { get; set; }
}

public class AddPersonCommandHandler(AppDbContext dbContext) : IRequestHandler<AddPersonCommand>
{
    public async Task Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        var group = await dbContext.Groups
            .Include(x=>x.Members)
            .FirstAsync(x => x.Id == request.GroupId, cancellationToken);
        var person = await dbContext.Persons
            .FirstAsync(x => x.Id == request.PersonId, cancellationToken);
        
        group.AddPerson(person);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}