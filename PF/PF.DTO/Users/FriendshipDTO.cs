using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.DTO.Users
{
    public class FriendshipDTO
    {
        public enum FriendshipStatus
        {
            Invited = 0,
            Accepted = 1
        }

        public long UserOneId { get; set; }
        public long UserTwoId { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
