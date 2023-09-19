namespace PrimitiveBankingSystem.Accounts;

public class BankAccount
{
    private static int s_accountNumber = 1234567890;
    public string Number { get; }
    public string Owner { get; }

    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var transaction in _allTransactions)
            {
                balance += transaction.Amount;
            }

            return balance;
        }
    }

    public BankAccount(string name, decimal initialBalance = 0)
    {
        Number = s_accountNumber.ToString();
        s_accountNumber++;
        Owner = name;

        switch (initialBalance)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(Balance), "Initial balance must be greater than zero.");
            case > 0:
                Deposit(initialBalance, DateTime.Now, "Initial balance");
                break;
        }
    }

    private readonly List<Transaction> _allTransactions = new List<Transaction>();

    public void Deposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than zero.");

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must ne greater than zero.");

        if ((Balance - amount) < 0)
            throw new InvalidOperationException("Insufficient funds.");

        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);
    }

    public void Transfer(BankAccount to, decimal amount, DateTime date, string note)
    {
        if (to == this)
            throw new InvalidOperationException("Cannot transfer to own account.");

        try
        {
            Withdraw(amount, DateTime.Now, $"Transfer out: {note}");
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"Transfer failed: {e.Message}");
            throw;
        }

        try
        {
            to.Deposit(amount, date, $"Transfer in: {note}");
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
            Deposit(amount, date, $"Transfer refund: {note}");
            throw;
        }
    }
    
    public virtual void PerformMonthEndTransactions() {}
}