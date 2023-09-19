namespace PrimitiveBankingSystem.Accounts;

public class InterestEarningAccount : BankAccount
{

    private decimal InterestPercent { get; }

    public InterestEarningAccount(string name, decimal initialBalance, decimal interestPercent = 0.02m) : base(name,
        initialBalance)
    {
        InterestPercent = interestPercent;
    }

    public override void PerformMonthEndTransactions()
    {
        if (!(Balance > 0)) return;
        var interest = Balance * InterestPercent;
        Deposit(interest, DateTime.Now, "Monthly interest credit");
    }
}