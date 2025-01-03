using FluentAssertions;
using SplitItUp.Domain;

namespace SplitItUp.Tests.Unit;

public class GroupAggregateTests
{
    [Fact]
    public void AddSpending_Should_AddSpendingSharesCorrectly()
    {
        // arrange
        var group = new Group("Testgroup")
        {
            Id = Guid.NewGuid()
        };
        var person1 = CreateTestPerson("Testi", "Testo"); // creator
        var person2 = CreateTestPerson("Testi", "Testo");
        var person3 = CreateTestPerson("Testi", "Testo");
        group.AddPerson(person1);
        group.AddPerson(person2);
        group.AddPerson(person3);

        var expectedSpeningShares = new List<SpendingShare>
        {
            new() { Amount = 33.33, PersonId = person1.Id, PercentageOfSpending = 33.33, Settled = false },
            new() { Amount = 33.33, PersonId = person2.Id, PercentageOfSpending = 33.33, Settled = false },
            new() { Amount = 33.33, PersonId = person3.Id, PercentageOfSpending = 33.33, Settled = false }
        };

        var expectedSpending = new Spending
        {
            Title = "Test",
            Amount = 100,
            PersonId = person1.Id,
            GroupId = group.Id,
            SpendingShares = expectedSpeningShares
        };


        // act
        group.AddSpending("Test", person1.Id, 100,
            new Dictionary<Guid, double> { { person1.Id, 33.33 }, { person2.Id, 33.33 }, { person3.Id, 33.33 } });

        // assert
        group.Spendings.Count.Should().Be(1);
        group.Spendings[0].SpendingShares.Count.Should().Be(3);
        group.Spendings[0].Should().BeEquivalentTo(expectedSpending, 
            options =>
            {
                 return options
                    .For(a => a.SpendingShares)
                    .Exclude(b => b.Spending);
            });
    }
    
    
    [Fact]
    public void AddSpending_Should_ThrowError_When_PersonIsNotInGroup()
    {
        // arrange
        var group = new Group("Testgroup")
        {
            Id = Guid.NewGuid()
        };
        var person1 = CreateTestPerson("Testi", "Testo"); // creator
        var person2 = CreateTestPerson("Testi", "Testo");
        var person3 = CreateTestPerson("Testi", "Testo");
        group.AddPerson(person1);
        group.AddPerson(person2);
        

        // act
        var action= ()=>group.AddSpending("Test", person1.Id, 100,
            new Dictionary<Guid, double> { { person1.Id, 33.33 }, { person2.Id, 33.33 }, { person3.Id, 33.33 } });

        // assert
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void AddSpending_Should_ThrowException_When_TotalPriceIsNotEqualToSumOfShares()
    {
        // arrange
        var group = new Group("Testgroup")
        {
            Id = Guid.NewGuid()
        };
        var person1 = CreateTestPerson("Testi", "Testo"); // creator
        var person2 = CreateTestPerson("Testi", "Testo");
        var person3 = CreateTestPerson("Testi", "Testo");
        group.AddPerson(person1);
        group.AddPerson(person2);
        group.AddPerson(person3);
        

        // act
        var action= ()=>group.AddSpending("Test", person1.Id, 100,
            new Dictionary<Guid, double> { { person1.Id, 33.33 }, { person2.Id, 33.33 }, { person3.Id, 33.43 } });

        // assert
        action.Should().Throw<ArgumentException>();
    }


    private static Person CreateTestPerson(string firstName, string lastName)
    {
        return new Person(firstName, lastName) { Id = Guid.NewGuid() };
    }
}