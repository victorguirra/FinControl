using FinControl.Areas.Identity.Data;
using FinControl.Data;
using FinControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinControl.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public CategoryController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(this.User);

            List<Category> allCategories = _context.Categories.Where(x => x.UserId == userId).ToList();
            return View(allCategories);
        }

        public IActionResult Details(int id)
        {
            Category category = _context.Categories.Find(id);
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            category.UserId = _userManager.GetUserId(this.User);
            
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            category.UserId = _userManager.GetUserId(this.User);

            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            return View(category);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            List<Transaction> allTransactionsOfCategory = _context.Transactions.Where(x => x.CategoryId == id).ToList();

            _context.Transactions.RemoveRange(allTransactionsOfCategory);
            _context.SaveChanges();

            Category category = _context.Categories.Find(id);

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
