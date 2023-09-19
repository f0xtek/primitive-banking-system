using PrimitiveBankingSystem.Accounts;
using static System.Console;

namespace PrimitiveBankingSystem;

public class Bank
{
    public void Run()
    {
        WriteLine("Welcome to Primitive Banking System!");
        var account1 = new BankAccount("Test Account 1");
        var account2 = new BankAccount("Test Account 2", 1000);
        
        account1.Deposit(200, DateTime.Now, "Sold some stuff");
        WriteLine($"account 1 ({account1.Number}) balance: {account1.Balance}");
        account1.Withdraw(50, DateTime.Now, "Petrol");
        WriteLine($"account 1 ({account1.Number}) balance: {account1.Balance}");

        try
        {
            account1.Withdraw(200, DateTime.Now, "Furniture");
        }
        catch (InvalidOperationException e)
        {
            WriteLine($"account 1 ({account1.Number}): {e.Message}");
        }
        
        account2.Deposit(200, DateTime.Now, "Sold some stuff");
        WriteLine($"account 2 ({account2.Number}) balance: {account2.Balance}");
        account2.Withdraw(50, DateTime.Now, "Petrol");
        WriteLine($"account 2 ({account2.Number}) balance: {account2.Balance}");

        try
        {
            var invalidAccount = new BankAccount("Invalid Account", -500);
        }
        catch (ArgumentOutOfRangeException e)
        {
            WriteLine("Cannot create account with negative balance.");
            WriteLine(e.Message);
        }
    }
}