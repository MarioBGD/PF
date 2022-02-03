using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.DTO.Expenses;
using PF.WebApi.WebApi.Authorization;
using PF.Common.Extensions;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/balances")]
    public class BalancesController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public BalancesController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalanceDTO>>> Get(string ids, Nullable<long> groupId)
        {
            await SessionManager.Authorize(HttpContext);

            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }

            List<long> idsList = ids.ToLongList();

            IEnumerable<BalanceDTO> balances = await _paymentService.GetBalances(idsList, groupId);
            return Ok(balances);
        }
    }
}
