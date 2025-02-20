using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Communication.requests;
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
            try
            {
                var useCase = new RegisterExpensesUseCase();

                var response = useCase.Execute(body: body);


                return Created(string.Empty, response);
            }
            catch (ArgumentException error) 
            {
                return BadRequest(error.Message);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "unknow Error");
            }
        }

    }
}
