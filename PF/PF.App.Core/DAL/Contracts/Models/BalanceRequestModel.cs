using System;
using System.Collections.Generic;

namespace PF.App.Core.DAL.Contracts.Models
{
    public class BalanceRequestModel
    {
        public List<long> UserIds { get; set; }
        public Nullable<long> GroupId { get; set; }
    }
}