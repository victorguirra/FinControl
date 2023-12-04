using FinControl.Areas.Identity.Data;
using FinControl.Data;
using FinControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinControl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(this.User);
            List<Account> accounts = _context.Accounts.Where(x => x.UserId == userId).ToList();

            return View(accounts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([Bind("AccountId,BankName,AccountType,BankBranch,AccountNumber")] Account account)
        {
            account.UserId = _userManager.GetUserId(this.User);

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Account account = _context.Accounts.Find(id);
            return View(account);
        }

        public IActionResult Edit(int id)
        {
            Account account = _context.Accounts.Find(id);
            return View(account);
        }

        [HttpPost]
        public IActionResult Update([Bind("AccountId,BankName,AccountType,BankBranch,AccountNumber")] Account account)
        {
            account.UserId = _userManager.GetUserId(this.User);

            _context.Accounts.Update(account);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Account account = _context.Accounts.Find(id);
            return View(account);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            List<Transaction> allTransactionsOfAccount = _context.Transactions.Where(x => x.AccountId == id).ToList();

            _context.Transactions.RemoveRange(allTransactionsOfAccount);
            _context.SaveChanges();

            Account account = _context.Accounts.Find(id);

            _context.Accounts.Remove(account);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
