using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {

        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterExpenseJson body)
        {

            var useCase = new RegisterExpensesUseCase();

            var response = useCase.Execute(body: body);


            return Created(string.Empty, response);
        }

    }
}
