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
using System.ComponentModel.DataAnnotations;


namespace Lab5.Models
{
    //Forgot password view model. Only an email text input
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
