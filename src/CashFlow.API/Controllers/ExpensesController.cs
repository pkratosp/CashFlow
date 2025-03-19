using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Communication.requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {

        [HttpPost]
        public IActionResult Register(
            [FromServices] IRegisterExpensesUseCase useCase,
            [FromBody] RequestRegisterExpenseJson body
        )
        {
            var response = useCase.Execute(body: body);


            return Created(string.Empty, response);
        }

    }
}
