using System;
using System.Collections.Generic;
using System.Text;

namespace PF.Mobile.App.Models
{
    public class BalanceRequestModel
    {
        public List<long> UserIds { get; set; }
        public Nullable<long> GroupId { get; set; }
    }
}
