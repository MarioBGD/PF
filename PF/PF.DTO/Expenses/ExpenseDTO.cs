using System;
using System.Collections.Generic;
using System.Text;

namespace PF.DTO.Expenses
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Mode { get; set; }
        public long GroupId { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<PaymentDTO> Payments { get; set; }
    }
}
