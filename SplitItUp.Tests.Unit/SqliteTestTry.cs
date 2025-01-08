using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SplitItUp.Domain;
using SplitItUp.Infrastructure;

namespace SplitItUp.Tests.Unit;

public class SqliteTestTry
{
    
    [Fact]
    public async Task UnitTestWithSqliteInMemory()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var context = new AppDbContext(contextOptions);
        
        await context.Database.EnsureCreatedAsync();
        
        context.Groups.Add(new Group("TestGroup"));
        await context.SaveChangesAsync();
        
        
        var groups = await context.Groups.ToListAsync();
        groups.Count.Should().Be(1);
    }
}