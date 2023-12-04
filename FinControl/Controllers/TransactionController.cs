using FinControl.Areas.Identity.Data;
using FinControl.Data;
using FinControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinControl.Controllers
{
    public class TransactionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public TransactionController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(this.User);

            List<Transaction> allTransactions = _context.Transactions
                .Include(x => x.Category)
                .Include(x => x.Account)
                .Where(x => x.UserId == userId)
                .ToList();

            return View(allTransactions);
        }

        public IActionResult Details(int id)
        {
            Transaction transaction = _context.Transactions
                .Include(x => x.Category)
                .Include(x => x.Account)
                .FirstOrDefault(x => x.TransactionId == id);
            return View(transaction);
        }

        public IActionResult Create()
        {
            string userId = _userManager.GetUserId(this.User);
            ViewBag.Categories = _context.Categories.Where(x => x.UserId == userId).ToList();
            ViewBag.Accounts = _context.Accounts.Where(x => x.UserId == userId).ToList();

            return View(new Transaction());
        }

        public IActionResult Add([Bind("TransactionId,CategoryId,AccountId,Amount,Note,Date")] Transaction transaction)
        {
            transaction.UserId = _userManager.GetUserId(this.User);

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            string userId = _userManager.GetUserId(this.User);
            ViewBag.Categories = _context.Categories.Where(x => x.UserId == userId).ToList();
            ViewBag.Accounts = _context.Accounts.Where(x => x.UserId == userId).ToList();

            Transaction transaction = _context.Transactions
                .Include(x => x.Category)
                .Include(x => x.Account)
                .FirstOrDefault(x => x.TransactionId == id);
            return View(transaction);
        }

        public IActionResult Update([Bind("TransactionId,CategoryId,AccountId,Amount,Note,Date")] Transaction transaction)
        {
            transaction.UserId = _userManager.GetUserId(this.User);

            _context.Transactions.Update(transaction);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Transaction transaction = _context.Transactions
                .Include(x => x.Category)
                .Include(x => x.Account)
                .FirstOrDefault(x => x.TransactionId == id);
            return View(transaction);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = _context.Transactions.Find(id);

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
