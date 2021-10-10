using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Entities
{
    public class GroupEntity : BaseEntity
    {
        public string Name { get; set; }
        public int DefaultCurrencyId { get; set; }
    }
}
