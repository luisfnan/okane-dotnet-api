using Okane.Contracts;
using Okane.Core.Services;
using Okane.Storage.InMemory;

namespace Okane.Tests.Services;

public class ExpensesServiceTests
{
    private readonly ExpensesService _expensesService;

    public ExpensesServiceTests()
    {
        _expensesService = new ExpensesService(new InMemoryExpensesRepository());
    }

    [Fact]
    public void RegisterExpense()
    {
        var expense = _expensesService.Register(new CreateExpenseRequest {
            Category = "Groceries",
            Amount = 10
        });
        
        Assert.Equal(1, expense.Id);
        Assert.Equal(10, expense.Amount);
        Assert.Equal("Groceries", expense.Category);
    }
    
    [Fact]
    public void RetrieveAllExpenses() {
        _expensesService.Register(new CreateExpenseRequest {
            Category = "Groceries",
            Amount = 10
        });

        _expensesService.Register(new CreateExpenseRequest {
            Category = "Entertainment",
            Amount = 20
        });

        var allExpenses = _expensesService.Retrieve();

        Assert.Equal(2, allExpenses.Count());
    }
    
    [Fact]
    public void RetrieveAllExpenses_FilterByCategory() {
        _expensesService.Register(new CreateExpenseRequest {
            Category = "Groceries",
            Amount = 10
        });

        _expensesService.Register(new CreateExpenseRequest {
            Category = "Entertainment",
            Amount = 20
        });

        var expenses = _expensesService.Retrieve("Groceries");

        var expense = Assert.Single(expenses);
        Assert.Equal("Groceries", expense.Category);
    }

    [Fact]
    public void GetById()
    {
        var createdExpense = _expensesService.Register(new CreateExpenseRequest {
            Category = "Groceries",
            Amount = 10
        });

        var retrievedExpense = _expensesService.ById(createdExpense.Id);
        
        Assert.Equal(createdExpense.Category, retrievedExpense.Category);
    }
}