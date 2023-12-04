using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinControl.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BankName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string AccountType { get; set; }
        
        [Column(TypeName = "nvarchar(4)")]
        public string BankBranch { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string AccountNumber { get; set; }

        public string UserId { get; set; }
    }
}
