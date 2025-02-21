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
            try
            {
                var useCase = new RegisterExpensesUseCase();

                var response = useCase.Execute(body: body);


                return Created(string.Empty, response);
            }
            catch (ErrorValidationException error) 
            {
                var errroMessage = new ResponseErrorJson(error.Errors);

                return BadRequest(errroMessage);
            }
            catch
            {
                var erroMessage = new ResponseErrorJson("unknow error");

                return StatusCode(StatusCodes.Status500InternalServerError, erroMessage);
            }
        }

    }
}
