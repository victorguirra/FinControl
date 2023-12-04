using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinControl.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string Note { get; set; }
        
        public DateTime Date { get; set; } = DateTime.Now;

        public string UserId { get; set; }
    }
}
