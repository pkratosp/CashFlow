using CashFlow.Application.useCases.Login;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(RequestUserLogin), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> login(
        [FromBody] RequestUserLogin body,
        [FromServices] ILoginUserUseCase useCase)
    {
        var response = await useCase.Execute(body);

        return Ok(response);
    }

}
