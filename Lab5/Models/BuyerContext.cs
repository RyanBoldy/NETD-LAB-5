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
    // Inherits debcontext
    public class BuyerContext : DbContext
    {
        //Constructor for Context
        public BuyerContext(DbContextOptions<BuyerContext> options): base(options)
        {



        }

        public DbSet<Buyer> Buyer { get; set; }

    }
}
