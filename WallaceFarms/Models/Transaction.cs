using System;
using System.Data.Entity;

namespace WallaceFarms.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public Customer customer { get; set; }
        public Cut cut { get; set; }
        public Order order { get; set; }
    }
    /*
    public class TransactionDBContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
    }*/
}