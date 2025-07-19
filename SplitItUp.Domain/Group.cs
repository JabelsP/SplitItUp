namespace SplitItUp.Domain;

public class Group
{
    private List<Person> _members = new();
    private List<Spending> _spendings = new();
    public Guid Id { get; set; }
    public string Name { get; set; }

    public IReadOnlyList<Person> Members => _members.AsReadOnly();

    public IReadOnlyList<Spending> Spendings => _spendings.AsReadOnly();

    public Group(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("name cant be null");
        }

        Name = name;
    }


    public void AddPerson(Person person)
    {
        _members.Add(person);
    }

    public void RemovePerson(Person person)
    {
        _members.Remove(person);
    }

    public void AddSpending(Spending spending)
    {
        _spendings.Add(spending);
    }

    public void RemoveSpending(Spending spending)
    {
        _spendings.Remove(spending);
    }

    public void AddSpending(string title, Guid personId, double totalPrice,
        Dictionary<Guid, double> requestSharesByPersonId)
    {
        if (_members.Count(x => x.Id == personId) != 1)
        {
            throw new ArgumentException("person not in group");
        }

        var spending = new Spending
        {
            Title = title,
            Amount = totalPrice,
            PersonId = personId,
            GroupId = Id,
        };
        var spendingShares = new List<SpendingShare>();

        var calculatedTotalAmount = 0.0;
        foreach (var shareByPersonId in requestSharesByPersonId)
        {
            if (_members.Count(x => x.Id == shareByPersonId.Key) != 1)
            {
                throw new ArgumentException("person not in group");
            }

            calculatedTotalAmount += shareByPersonId.Value;
            spendingShares.Add(new SpendingShare
            {
                PersonId = shareByPersonId.Key,
                Amount = shareByPersonId.Value,
                Spending = spending,
                PercentageOfSpending = Math.Round(shareByPersonId.Value / totalPrice, 4)*100
            });
        }

        if (Math.Abs(totalPrice - calculatedTotalAmount) >= 0.0199)
        {
            throw new ArgumentException("shares do not sum up to amount of spending");
        }

        spending.SpendingShares = spendingShares;
        _spendings.Add(spending);
    }
}