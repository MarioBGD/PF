using System;
using System.Collections.Generic;
using System.Text;

namespace PF.DTO.Expenses
{
    public class PaymentDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public long ExpenseId { get; set; }

        public int OrginalCurrencyId { get; set; }
        public decimal OrginalAmount { get; set; }
    }
}
