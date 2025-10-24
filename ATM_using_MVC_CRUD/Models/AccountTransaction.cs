using System.ComponentModel.DataAnnotations;

namespace ATM_using_MVC_CRUD.Models
{
    public class AccountTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int AccountNumber { get; set; }

        public decimal Amount { get; set; }

        public string? Type { get; set; } // Deposit / Withdraw

        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}
