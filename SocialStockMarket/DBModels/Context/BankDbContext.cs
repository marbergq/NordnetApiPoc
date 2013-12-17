using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SocialStockMarket.DBModels.Context
{
    public class BankDbContext : DbContext
    {
        
        public DbSet<BankUser> BankUsers { get; set; }
    }
}