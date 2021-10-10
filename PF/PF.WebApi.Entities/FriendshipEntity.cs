using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Entities
{
    public class FriendshipEntity : BaseEntity
    {
        public long UserOneId { get; set; }
        public long UserTwoId { get; set; }
        public int Status { get; set; }
    }
}
