namespace SplitItUp.Application.Groups.DTOs;

public class GroupOverviewDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    // public required PersonDto CreatedBy { get; set; }
    public required List<PersonDto> Members { get; set; }
}

public class PersonDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}