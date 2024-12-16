using System.Diagnostics;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpenseDbContext _expenseDbContext;

        public HomeController(ILogger<HomeController> logger, ExpenseDbContext expenseDbContext)
        {
            _logger = logger;
            _expenseDbContext = expenseDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _expenseDbContext.Expenses.ToList();
            var totalExpense = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpense;
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var expenseInDb = _expenseDbContext.Expenses.SingleOrDefault(x => x.Id == id);
                return View(expenseInDb);
            }
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _expenseDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            _expenseDbContext.Expenses.Remove(expenseInDb);
            _expenseDbContext.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense expense)
        {
            if (expense.Id == 0)
            {
                _expenseDbContext.Expenses.Add(expense);
            }
            else
            {
                _expenseDbContext.Expenses.Update(expense);
            }
            _expenseDbContext.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
