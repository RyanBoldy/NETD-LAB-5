/**
 * Ryan Boldy
 * NETD
 * 12/12/2022
 * Lab 5
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Lab5.Models
{
    //Inherits from Dbcontext
    public class StockContext : DbContext
    {
        //Constructor for CarContext
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {



        }
        public DbSet<Stock> Stocks { get; set; }

    }
}
