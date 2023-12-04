using FinControl.Models;

namespace FinControl.ViewModels
{
    public class DashboardViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal Balance { get; set; }
    }
}
