// // using SplitItUp.Application;
// using SplitItUp.Application.Persons;
//
// namespace SplitItUp.Infrastructure.Repositories;
//
// public class UnitOfWork (AppDbContext dbContext):IUnitOfWork
// {
//     private IPersonRepository? _personRepository;
//
//     public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(dbContext);
//
//     public void Commit(CancellationToken cancellationToken)
//     {
//         dbContext.SaveChangesAsync(cancellationToken);
//     }
// }
//
