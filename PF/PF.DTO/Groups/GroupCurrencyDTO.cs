using System;
using System.Collections.Generic;
using System.Text;

namespace PF.DTO.Groups
{
    public class GroupCurrencyDTO
    {
        public int CurrencyId { get; set; }
        public decimal Worth { get; set; }
        public long GroupId { get; set; }
    }
}
