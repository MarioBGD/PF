using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Entities
{
    public class PaymentEntity : BaseEntity
    {
        public decimal Amount { get; set; }
        public long ExpenseId { get; set; }
        public long UserId { get; set; }
        public int OrginalCurrencyId { get; set; }
        public decimal OrginalAmount { get; set; }
    }
}
