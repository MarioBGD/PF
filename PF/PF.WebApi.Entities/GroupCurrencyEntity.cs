using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Entities
{
    public class GroupCurrencyEntity : BaseEntity
    {
        public int CurrencyId { get; set; }
        public decimal Worth { get; set; }
        public long GroupId { get; set; }
    }
}
