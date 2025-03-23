using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredExpensesJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterExpensesUseCase useCase,
            [FromBody] RequestRegisterExpenseJson body
        )
        {
            var response = await useCase.Execute(body: body);


            return Created(string.Empty, response);
        }

    }
}
