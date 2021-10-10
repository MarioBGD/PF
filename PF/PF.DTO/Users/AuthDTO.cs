using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.DTO.Users
{
    public class AuthDTO
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public long UserId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
