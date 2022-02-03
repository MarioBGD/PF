using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public class ExpenseEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Mode { get; set; }
        public long GroupId { get; set; }
    }
}
