using NordnetPoC.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialStockMarket.DBModels
{
    public class BankUser
    {
        [Key]
        [Column(Order=0)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserIdentity { get; set; }
        [Key]
        [Column(Order=1)]
        public string UserName { get; set; }
        public virtual List<Folder> StockFolders { get; set; }
    }

    public class Folder
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public virtual List<UserStock> StocksInFolder { get; set; }
    
    }


    public class UserStock 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
       public int id { get; set; }
        public string StockId { get; set; }
        public string StockName { get; set; }
        public double LatestPrice { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public string Currency { get; set; }
        public double UserInvestment { get; set; }
        public DateTime LastUpdateTime { get; set; }

    }

    public class UserStockForDb : UserStock
    {
        public UserStockForDb(IStock stock)
        {
            id = stock.InfoLink.First().Value.GetHashCode();
            StockName = stock.InfoLink.First().Value;
            LatestPrice = double.Parse(stock.LatestPrice);
            //ask
            //bid
            Currency = stock.Currency;
            UserInvestment = double.Parse(stock.Count) * LatestPrice;
        }
    }

}