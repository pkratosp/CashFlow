using CashFlow.Application.useCases.User.Register;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(RequestRegisterUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(
        [FromServices] IRegisterUserUseCase usecase,
        [FromBody] RequestRegisterUserJson body
    )
    {
        var result = await usecase.Execute(body);


        return Ok(result);
    }

}
