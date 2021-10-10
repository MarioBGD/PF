using System;
using System.Collections.Generic;
using System.Text;

namespace PF.DTO.Groups
{
    public class MembershipDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }

        /// <summary>
        /// 0 - invited, 1 - normal, 2 - admin, 3 - owner
        /// </summary>
        public int Status { get; set; }
    }
}
