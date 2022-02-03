using Microsoft.AspNetCore.Mvc;
using PF.DTO.Expenses;
using PF.WebApi.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.Common.Extensions;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> Get(Nullable<long> groupId, string ids = null)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);
            IEnumerable<ExpenseDTO> expenses = null;

            if (!string.IsNullOrEmpty(ids))
            {
                List<long> idsList = ids.ToLongList();
                expenses = await _expenseService.GetExpenses(idsList.AsEnumerable());
            }
            else if (groupId.HasValue)
            {
                expenses = await _expenseService.GetAllExpensesOfGroup(sourceUserId, groupId.Value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(expenses);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> Update([FromBody] ExpenseDTO expense)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            await _expenseService.UpdateExpense(expense);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> Add([FromBody] ExpenseDTO expense)
        {
            long sourceUserId = await SessionManager.Authorize(HttpContext);

            await _expenseService.AddExpense(expense);

            return Ok();
        }
    }
}
