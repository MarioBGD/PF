using AutoMapper;
using PF.DTO.Expenses;
using PF.WebApi.DAL.Core;
using PF.WebApi.DAL.Core.Repositories;
using PF.WebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.BLL.Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;

        public ExpenseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpensesOfGroup(long sourceUserId, long groupId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                ExpensesRepository expensesRepository = new ExpensesRepository(uow);
                IEnumerable<ExpenseEntity> expensesEntities = await expensesRepository.GetAllExpensesOfGroup(groupId);
                IEnumerable<ExpenseDTO> expenses = _mapper.Map<IEnumerable<ExpenseDTO>>(expensesEntities);

                if (expenses.Count() > 0)
                {
                    PaymentsRepository paymentsRepository = new PaymentsRepository(uow);
                    IEnumerable<long> expensesIds = expenses.Select(x => x.Id);
                    IEnumerable<PaymentEntity> paymentsEntities = await paymentsRepository.GetPayments(expensesIds);
                    IEnumerable<PaymentDTO> payments = _mapper.Map<IEnumerable<PaymentDTO>>(paymentsEntities);

                    foreach (var expense in expenses)
                    {
                        var expensePayments = payments.Where(x => x.ExpenseId == expense.Id);
                        expense.Payments = expensePayments.ToList();
                    }
                }

                return expenses;
            }
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenses(IEnumerable<long> ids)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                ExpensesRepository expensesRepository = new ExpensesRepository(uow);
                IEnumerable<ExpenseEntity> entites = await expensesRepository.GetRange(ids);
                IEnumerable<ExpenseDTO> expenses = _mapper.Map<IEnumerable<ExpenseDTO>>(entites);
                return expenses;
            }
        }


        public async Task<bool> AddExpense(ExpenseDTO expense)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                ExpenseEntity entity = _mapper.Map<ExpenseEntity>(expense);
                ExpensesRepository expensesRepository = new ExpensesRepository(uow);
                long expenseId = await expensesRepository.Add(entity);

                var payments = expense.Payments.AsEnumerable();

                foreach(var payment in payments)
                {
                    payment.ExpenseId = expenseId;
                }

                var paymentEntities = _mapper.Map<IEnumerable<PaymentEntity>>(payments);
                PaymentsRepository paymentsRepository = new PaymentsRepository(uow);
                await paymentsRepository.AddRange(paymentEntities);

                uow.Commit();
            }

            return true;
        }

        public async Task<bool> DeleteExpense(long expenseId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateExpense(ExpenseDTO expense)
        {
            throw new NotImplementedException();
        }
    }
}
