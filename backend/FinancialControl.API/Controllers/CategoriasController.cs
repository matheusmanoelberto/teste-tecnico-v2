using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Application.Services;
using FinancialControl.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoriaDTO categoriaDto)
    {
        try
        {
            var created = await _service.AddAsync(categoriaDto);
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
