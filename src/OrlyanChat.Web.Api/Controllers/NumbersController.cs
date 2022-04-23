using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OrlyanChat.Web.Api.Services.Rng;

namespace OrlyanChat.Web.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("numbers")]
public sealed class NumbersController : Controller
{
    private readonly IRngService rngService;

    public NumbersController(IRngService rngService)
    {
        this.rngService = rngService ?? throw new ArgumentNullException(nameof(rngService));
    }

    [HttpGet]
    public async Task<IActionResult> GetSomeNumber()
    {
        var someNumber = await rngService.GetSomeNumber();

        return Ok(someNumber);
    }
}
