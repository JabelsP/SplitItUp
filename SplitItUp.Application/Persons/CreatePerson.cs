using MediatR;
using SplitItUp.Domain;
using SplitItUp.Infrastructure;

namespace SplitItUp.Application.Persons;

public class CreatePersonCommand : IRequest<Person>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class CreatePersonCommandHandler(AppDbContext dbContext) : IRequestHandler<CreatePersonCommand, Person>
{
    public async Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person(request.FirstName, request.LastName);
        var createdPerson = await dbContext.Persons.AddAsync(person, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return createdPerson.Entity;
    }
}