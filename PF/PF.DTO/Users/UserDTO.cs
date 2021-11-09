using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.DTO.Users
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string AvatarSrc { get; set; }
    }
}
