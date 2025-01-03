namespace SplitItUp.Domain;

public class SpendingShare
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public double PercentageOfSpending { get; set; }
    public bool Settled { get; set; }
    public DateTime? SettledAt { get; set; }
    
    public Spending Spending { get; set; }
    public Guid SpendingId { get; set; }
    
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}