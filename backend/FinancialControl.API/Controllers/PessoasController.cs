using System;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Application.Services;
using Microsoft.AspNetCore.Mvc;
using FinancialControl.Domain.Exceptions;

namespace FinancialControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _service;

    public PessoasController(IPessoaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pessoa = await _service.GetByIdAsync(id);
        if (pessoa == null) return NotFound();
        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PessoaDTO pessoaDto)
    {
        try
        {
            var created = await _service.AddAsync(pessoaDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PessoaDTO pessoaDto)
    {
        try
        {
            await _service.UpdateAsync(id, pessoaDto);
            return NoContent();
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
