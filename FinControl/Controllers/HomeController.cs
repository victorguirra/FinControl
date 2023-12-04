using FinControl.Areas.Identity.Data;
using FinControl.Data;
using FinControl.Models;
using FinControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinControl.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(this.User);
            List<Transaction> allTransactions = _context.Transactions
                .Include(x => x.Category).Where(x => x.UserId == userId).ToList();

            decimal totalExpense = allTransactions.Where(x => x.Category.Type == "Despesa").Sum(x => x.Amount);
            decimal totalIncome = allTransactions.Where(x => x.Category.Type == "Renda").Sum(x => x.Amount);
            decimal balance = totalIncome - totalExpense;
            decimal savings = balance > 0 ? (15 * balance) / 100 : 0;

            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.Transactions = allTransactions;
            dashboardViewModel.TotalExpense = totalExpense;
            dashboardViewModel.TotalIncome = totalIncome;
            dashboardViewModel.Balance = balance;
            dashboardViewModel.Savings = savings;

            return View(dashboardViewModel);
        }
    }
}