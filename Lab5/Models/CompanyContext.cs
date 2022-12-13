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
    //CompanyContext inherits from dbcontext
    public class CompanyContext : DbContext
    {
        //Constructor for Context
        public CompanyContext(DbContextOptions<CompanyContext> options): base(options)
        {



        }

        public DbSet<Company> Company { get; set; }

    }
}
