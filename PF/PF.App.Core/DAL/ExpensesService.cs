using System;
using System.Collections.Generic;
using System.Threading;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL
{
    public class ExpensesService : IExpensesServcie
    {
        public void GetExpenses(IExpensesServcie.OnExpensesDataUpdate callback, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void GetTest(IExpensesServcie.OnExpensesDataUpdate callback)
        {
            var expenses = new []
            {
                new Expense { Title = "Expense 1", CreatedDate = DateTime.Now, Payments = new List<Payment>
                    {
                        new Payment { MemberId = 1, Amount = 12m }
                    }},
                new Expense { Title = "Expense 2", CreatedDate = DateTime.Now - TimeSpan.FromHours(23), Payments = new List<Payment>
                    {
                        new Payment { MemberId = 2, Amount = 12.34m }
                    }},
                new Expense { Title = "Expense 3", CreatedDate = DateTime.Now - TimeSpan.FromHours(24), Payments = new List<Payment>
                    {
                        new Payment { MemberId = 3, Amount = -123m }
                    }},
                new Expense { Title = "Expense 4", CreatedDate = DateTime.Now - TimeSpan.FromDays(2), Payments = new List<Payment>
                    {
                        new Payment { MemberId = 4, Amount = 3.45m }
                    }},
                new Expense { Title = "Expense 5", CreatedDate = DateTime.Now - TimeSpan.FromDays(4), Payments = new List<Payment>
                    {
                        new Payment { MemberId = 5, Amount = 2m }
                    }},
                new Expense { Title = "Expense 6", CreatedDate = DateTime.Now - TimeSpan.FromDays(35), Payments = new List<Payment>
                    {
                        new Payment { MemberId = 6, Amount = -2m }
                    }},
            };

            callback.Invoke(expenses);
        }
    }
}