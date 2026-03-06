using System.Threading.Tasks;
using FinancialControl.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _service;

    public RelatoriosController(IRelatorioService service)
    {
        _service = service;
    }

    [HttpGet("pessoas")]
    public async Task<IActionResult> GetTotaisPorPessoa()
    {
        return Ok(await _service.GetTotaisPorPessoaAsync());
    }

    [HttpGet("geral")]
    public async Task<IActionResult> GetTotalGeral()
    {
        return Ok(await _service.GetTotalGeralAsync());
    }
}
