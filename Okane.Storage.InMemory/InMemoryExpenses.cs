using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public class InMemoryExpensesRepository : IExpensesRepository
{
    private readonly IList<Expense> _expenses;
    private int _nextId = 1;
    public InMemoryExpensesRepository() : this(new List<Expense>())
    {
    }

    private InMemoryExpensesRepository(IList<Expense> expenses) =>
        _expenses = expenses;

    public void Add(Expense expense)
    {
        expense.Id = _nextId++;
        _expenses.Add(expense);
    }

    public IEnumerable<Expense> All() => _expenses;
    public IEnumerable<Expense> ByCategory(string category) => 
        _expenses.Where(expense => expense.Category == category);

    public Expense? FilterByCategory(string category) =>
        _expenses.FirstOrDefault(expense => expense.Category == category);

    public Expense? ById(int id) => _expenses.FirstOrDefault(expense => expense.Id == id);
}
