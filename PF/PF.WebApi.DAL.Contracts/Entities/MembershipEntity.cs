using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public class MembershipEntity : BaseEntity
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }

        /// <summary>
        /// 0 - invited, 1 - normal, 2 - admin, 3 - owner
        /// </summary>
        public int Status { get; set; }
    }
}
