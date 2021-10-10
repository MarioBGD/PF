using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
