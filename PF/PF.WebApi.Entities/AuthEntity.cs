using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Entities
{
    public class AuthEntity : BaseEntity
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public long UserId { get; set; }
    }
}
