using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PF.WebApi.WebApi.Authorization
{
    public class Session
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        //public DateTime ExpiredTime { get; set; }
    }
}
