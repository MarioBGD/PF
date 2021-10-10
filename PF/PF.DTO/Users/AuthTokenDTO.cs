using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.DTO.Users
{
    public class AuthTokenDTO
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
