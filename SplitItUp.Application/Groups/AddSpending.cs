using MediatR;
using Microsoft.EntityFrameworkCore;
using SplitItUp.Infrastructure;

namespace SplitItUp.Application.Groups;

public class AddSpendingCommand : IRequest
{
    public required Guid GroupId { get; set; }
    public required Guid PersonId { get; set; }
    public required string Title { get; set; }
    public required double TotalPrice { get; set; }
    public required Dictionary<Guid, double> SharesByPersonId { get; set; }
}

public class AddSpendingCommandHandler(AppDbContext dbContext) : IRequestHandler<AddSpendingCommand>
{
    public async Task Handle(AddSpendingCommand request, CancellationToken cancellationToken)
    {
        var group = await dbContext.Groups
            .Include(x => x.Spendings)
            .Include(x => x.Members)
            .FirstAsync(x => x.Id == request.GroupId, cancellationToken);
        
        group.AddSpending(request.Title, request.PersonId, request.TotalPrice, request.SharesByPersonId);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}