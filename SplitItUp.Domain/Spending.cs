
namespace SplitItUp.Domain;

public class Spending
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public double Amount { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
    public Group Group { get; set; }
    public Guid GroupId { get; set; }
    public List<SpendingShare> SpendingShares { get; set; }
}

