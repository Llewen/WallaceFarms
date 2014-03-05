using System;
using System.Data.Entity;

namespace WallaceFarms.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int NumCows { get; set; }
    }

    public class TransactionDBContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
    }
}