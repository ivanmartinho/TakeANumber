using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeANumber.Data;
using TakeANumber.Extensions;
using TakeANumber.Models;
using TakeANumber.ViewModels;

namespace TakeANumber.Controllers;

[ApiController]
public class TicketGroupController : ControllerBase
{
    [HttpGet("v1/ticketgroups")]
    public async Task<IActionResult> GetAsync(
        [FromServices] TakeANumberDataContext context,
        [FromRoute]int page = 0,
        [FromRoute]int pageSize = 25)
    {
        try
        {
            var count = await context.TicketGroups.AsNoTracking().CountAsync();
            var ticketGroups = await context
                .TicketGroups
                .AsNoTracking()
                .Select(x=>new ListTicketGroupsViewModel
                {
                    Id= x.Id,
                    Name = x.Name,
                    Acronym = x.Acronym
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                data = ticketGroups
            }));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<ListTicketGroupsViewModel>>("TGX01 - Falha interna no servidor"));
        }
    }

    [HttpGet("v1/ticketgroups/{id:int}")]
    public async Task<IActionResult> GetbyIdAsync(
        [FromServices] TakeANumberDataContext context,
        [FromRoute]int id)
    {
        try
        {
            var ticketGroup = await context.TicketGroups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ticketGroup == null)
                return NotFound(new ResultViewModel<TicketGroup>("Grupo de ticket não foi localizado"));

            return Ok(new ResultViewModel<TicketGroup>(ticketGroup));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketGroup>("TGX02 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/ticketgroups")]
    public async Task<IActionResult> PostAsync(
        [FromServices] TakeANumberDataContext context,
        [FromBody] EditorTicketGroupViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<TicketGroup>(ModelState.GetErrros()));

        try
        {
            var ticketgroup = new TicketGroup()
            {
                Name = model.Name,
                Acronym = model.Acronym
            };

            await context.TicketGroups.AddAsync(ticketgroup);
            await context.SaveChangesAsync();

            return Created($"v1/ticketgroups/{ticketgroup.Id}", new ResultViewModel<TicketGroup>(ticketgroup));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<TicketGroup>("TGX03 - Falha interna no servidor"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketGroup>("TGX04 - Falha interna no servidor"));
        }
    }

    [HttpPut("v1/ticketgroups/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] TakeANumberDataContext context,
        [FromBody] EditorTicketGroupViewModel model,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<TicketGroup>(ModelState.GetErrros()));

        var ticketGroup = await context.TicketGroups.FirstOrDefaultAsync(x => x.Id == id);
        if (ticketGroup == null)
            return NotFound(new ResultViewModel<TicketGroup>("Grupo não foi localizado"));

        try
        {
            ticketGroup.Name = model.Name;
            ticketGroup.Acronym = model.Acronym;

            context.TicketGroups.Update(ticketGroup);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<TicketGroup>(ticketGroup));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<TicketGroup>("TGX05 - Falha interna no servidor"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketGroup>("TGX06 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/ticketgroups/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices]TakeANumberDataContext context,
        [FromRoute]int id)
    {
        var ticketGroup = await context.TicketGroups.FirstOrDefaultAsync(x => x.Id == id);
        if (ticketGroup == null)
            return NotFound(new ResultViewModel<TicketGroup>(ModelState.GetErrros()));

        try
        {
            context.TicketGroups.Remove(ticketGroup);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<TicketGroup>(ticketGroup));
        }
        catch(DbUpdateException)
        {
            return StatusCode(500, "TGX07 - Falha interna no servidor");
        }
        catch (Exception)
        {
            return StatusCode(500, "TGX08 - Falha interna no servidor");
        }
    }
}
