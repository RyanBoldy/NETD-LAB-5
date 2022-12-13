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
using System.ComponentModel;

namespace Lab5.Models
{
    //Companies sell stock to buyers
    public class Company
    {
        [DisplayName("Company ID")]
        public int CompanyId { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
    }
}
