using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]  // localhost:1234/api/qotd
[ApiController]
public class QotdController(ILogger<QotdController> logger, IQotdService qotdService) : ControllerBase
{
    [HttpGet] // localhost:1234/api/qotd
    public async Task<ActionResult<QuoteOfTheDayViewModel>> GetQuoteOfTheDay()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDay)} aufgerufen...");

        var qotdVm = await qotdService.GetQuoteOfTheDayAsync();

        return Ok(qotdVm);
    }
}