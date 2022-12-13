/**
 * Ryan Boldy
 * NETD
 * 12/12/2022
 * Lab 5
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    //Stocks are purchsed by buyers, from companies.
    //Stocks are created in the database, and take foreign keys from
    //the buyers table as well as the companies table so that the data can
    //stay organized and it is easy to track where the stock sales came from
    public class Stock
    {
        [DisplayName("Stock ID")]
        public int StockId { get; set; }
        [DisplayName("Company ID")]
        public int CompanyId { get; set; }
        [DisplayName("Company")]
        public Company Companys { get; set; }
        [DisplayName("Buyer ID")]
        public int BuyerId { get; set; }
        [DisplayName("Buyer")]
        public Buyer Buyers { get; set; }
        [DisplayName("Buy Price")]
        public double BuyPrice { get; set; }
        [DisplayName("Buy Date")]
        public DateTime BuyDate { get; set; }

    }

}
