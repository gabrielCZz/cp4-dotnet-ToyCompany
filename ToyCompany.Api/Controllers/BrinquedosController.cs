using Microsoft.AspNetCore.Mvc;
using ToyCompany.Application.DTOs;
using ToyCompany.Application.Interfaces;

namespace ToyCompany.Api.Controllers;

[ApiController]
[Route("brinquedos")]
[Produces("application/json")]
public sealed class BrinquedosController : ControllerBase
{
    private readonly IBrinquedoService _brinquedoService;

    public BrinquedosController(IBrinquedoService brinquedoService)
    {
        _brinquedoService = brinquedoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<BrinquedoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<BrinquedoDto>>> GetAll(CancellationToken cancellationToken)
    {
        var brinquedos = await _brinquedoService.GetAllAsync(cancellationToken);
        return Ok(brinquedos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BrinquedoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BrinquedoDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var brinquedo = await _brinquedoService.GetByIdAsync(id, cancellationToken);

        if (brinquedo is null)
        {
            return NotFound(new { message = $"Brinquedo com id {id} nao foi encontrado." });
        }

        return Ok(brinquedo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BrinquedoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BrinquedoDto>> Create([FromBody] CreateBrinquedoRequest request, CancellationToken cancellationToken)
    {
        var brinquedo = await _brinquedoService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = brinquedo.Id }, brinquedo);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BrinquedoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BrinquedoDto>> Update(int id, [FromBody] UpdateBrinquedoRequest request, CancellationToken cancellationToken)
    {
        var brinquedo = await _brinquedoService.UpdateAsync(id, request, cancellationToken);

        if (brinquedo is null)
        {
            return NotFound(new { message = $"Brinquedo com id {id} nao foi encontrado." });
        }

        return Ok(brinquedo);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _brinquedoService.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound(new { message = $"Brinquedo com id {id} nao foi encontrado." });
        }

        return NoContent();
    }
}
