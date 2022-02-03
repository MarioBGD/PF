using System;
using System.Collections.Generic;

namespace PF.App.Core.DAL.Contracts.Models
{
    public class Expense
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Payment> Payments { get; set; }
    }
}