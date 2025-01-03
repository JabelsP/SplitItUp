namespace SplitItUp.Domain;


public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Group> Groups { get; set; } = new();
    public List<Spending> Spendings { get; set; } = new();
    public List<SpendingShare> SpendingShares { get; set; } = new();
    
    public Person(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("name cant be null");
        }
        
        FirstName = firstName;
        LastName = lastName;
    }

}