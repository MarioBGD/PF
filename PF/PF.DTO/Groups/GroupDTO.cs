using System;
using System.Collections.Generic;
using System.Text;

namespace PF.DTO.Groups
{
    public class GroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int DefaultCurrencyId { get; set; }
        public List<GroupCurrencyDTO> Currencies { get; set; }
    }
}
