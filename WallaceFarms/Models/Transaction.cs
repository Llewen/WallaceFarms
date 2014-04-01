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
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        /* String formating by Sean on stackoverflow.com */
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Number of Cows")]
        public int NumCows { get; set; }
    }

    public class TransactionDBContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
    }
}