/**
 * Ryan Boldy
 * NETD
 * 12/12/2022
 * Lab 5
 */
using System;

namespace Lab5.Models
{
    //ErrorViewModel
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
