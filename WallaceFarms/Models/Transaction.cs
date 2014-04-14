using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WallaceFarms.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        /* Regex code by Dave on stackoverflow.com */
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$", ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(10, ErrorMessage = "Enter a VALID phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Cut")]
        [Range(1, 3, ErrorMessage = "Must be a value 1-3")]
        public int cut { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(1, 3, ErrorMessage = "Must be a value 1-3")]
        public int amount { get; set; }

        public string Status { get; set; }

    }

    public class TransactionDBContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
    }
}