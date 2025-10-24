using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_using_MVC_CRUD.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Primary key

        [Required]
        public int AccountNumber { get; set; } 
        [Required]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "PIN must be 4 digits", MinimumLength = 4)]
        public string? Pin { get; set; }
    }
}
