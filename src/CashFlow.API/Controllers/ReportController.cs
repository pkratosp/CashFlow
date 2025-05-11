using System.Net.Mime;
using CashFlow.Application.useCases.Expenses.Reports.Excel;
using CashFlow.Application.useCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcell(
            [FromHeader] DateOnly month,
            [FromServices] IGenerateExpensesReportUseCase useCase
        )
        {

            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
            }

            return NoContent();
        }

        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPDF(
            [FromQuery] DateOnly month,
            [FromServices] IGenerateExpensesReportPdfUseCase useCase
        )
        {

            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
            }

            return NoContent();
        }

    }
}
