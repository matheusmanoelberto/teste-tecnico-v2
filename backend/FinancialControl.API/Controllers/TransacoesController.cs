using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Application.Services;
using FinancialControl.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _service;

    public TransacoesController(ITransacaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TransacaoDTO transacaoDto)
    {
        try
        {
            var created = await _service.AddAsync(transacaoDto);
            return Ok(created);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
