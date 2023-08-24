using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public class ExpensesRepository : IExpensesRepository
{
    private readonly OkaneDbContext _db;

    public ExpensesRepository(OkaneDbContext db) => _db = db;

    public void Add(Expense expense)
    {
        _db.Expenses.Add(expense);
        _db.SaveChanges();
    }

    public IEnumerable<Expense> All() => 
        _db.Expenses;

    public IEnumerable<Expense> ByCategory(string category) => 
        _db.Expenses.Where(expense => expense.Category == category);

    public Expense? FilterByCategory(string category)
    {
        return _db.Expenses.FirstOrDefault(expense => expense.Category == category);
    }


    public Expense? ById(int id) => 
        _db.Expenses.FirstOrDefault(expense => expense.Id == id);
}