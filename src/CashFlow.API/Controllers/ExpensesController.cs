using CashFlow.Application.useCases.Expenses.GetAll;
using CashFlow.Application.useCases.Expenses.GetById;
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


        [HttpGet]
        [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase)
        {
            var response = await useCase.Execute();

            if(response.Expenses.Count != 0)
            {
                return Ok(response);
            }


            return NoContent();
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetByIdExpenseUseCase useCase,
            [FromRoute] long id
        )
        {

            var response = await useCase.Execute(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound();
        }
    }
}
