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
    //Model for the buyer object. Buyers represent people on the system
    //Who are capable of buying stock from companies
    public class Buyer
    {
        [DisplayName("Buyer ID")]
        public int BuyerId { get; set; }
        [DisplayName("Buyer Name")]
        public string BuyerName { get; set; }
    }
}
