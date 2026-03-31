using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeANumberApi.Data;
using TakeANumberApi.DTOs;
using TakeANumberApi.Extensions;
using TakeANumberApi.Models;
using TakeANumberShared.Enums;
using TakeANumberShared.ViewModels;

namespace TakeANumberApi.Controllers;
[ApiController]
public class TicketNumberController : ControllerBase
{
    [HttpGet("v1/ticketnumbers")]
    public async Task<IActionResult> GetNextAsync(
        [FromServices] TakeANumberDataContext context,
        [FromBody] TicketNumberRequest request)
    {
        try
        {
            var ticket = await context.TicketNumbers
            .FirstOrDefaultAsync(x => x.TicketType == request.TicketType
                    && x.TicketGroup.Id == request.TicketGroupId
                    && x.Spot.Id == request.SpotId
                    && x.Called == false);

            var response = new TicketNumberResponse
            {
                Id = ticket.Id,
                SpotName = ticket.Spot.Name,
                TicketNumber = ticket.Ticket
            };

            ticket.Called = true;
            ticket.CalledDate = DateTime.UtcNow;

            context.TicketNumbers.Update(ticket);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<TicketNumberResponse>(response));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX01 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/ticketnumbers")]
    public async Task<IActionResult> PostAsync(
        [FromServices] TakeANumberDataContext context,
        [FromBody] TicketNumberRequest request)
    {
        var model = new EditorTicketNumberViewModel
        {
            TicketGroupId = request.TicketGroupId,
            SpotId = request.SpotId ,
            CompanyId = request.CompanyId ,
            TicketType = request.TicketType,
        };

        if (!TryValidateModel(model))
            return BadRequest(new ResultViewModel<TicketNumberResponse>(ModelState.GetErrros()));

        try
        {
            var ticketGroup = await context.TicketGroups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.TicketGroupId);
            var number = await context.TicketNumbers.CountAsync();
            var ticket = new TicketNumber()
            {
                Ticket = ticketGroup?.Acronym + number.ToString("D"),
                TicketGroup = ticketGroup,
                Spot = new Spot { Id = request.SpotId },
                Company = new Company { Id = request.CompanyId },
                TicketType = request.TicketType,
            };

            await context.TicketNumbers.AddAsync(ticket);
            await context.SaveChangesAsync();
            var teste = TicketType.Priority;
            var retorno = teste.ToString();
            var reponse = new TicketNumberResponse(ticket.Id, ticket.Spot.Name, ticket.Ticket, ticket.TicketType.GetDisplayName());

            return Ok(new ResultViewModel<TicketNumberResponse>(reponse));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX02 - Falha interna no servidor"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX03 - Falha interna no servidor"));
        }
    }

    [HttpPut("v1/ticketnumbers/call/{ticketNumber}")]
    public async Task<IActionResult> CallAsync(
        [FromServices] TakeANumberDataContext context,
        [FromRoute] string ticketNumber,
        [FromBody] TicketNumberRequest request)
    {
        try
        {
            var ticket = await context.TicketNumbers.FirstOrDefaultAsync(x => x.Ticket == ticketNumber);

            if (ticket == null)
                return BadRequest(new ResultViewModel<TicketNumberResponse>("Ticket não foi encontrado"));

            if (ticket.Called)
                return BadRequest(new ResultViewModel<TicketNumberResponse>("Ticket já foi chamado"));

            ticket.Called = true;
            ticket.CalledDate = DateTime.UtcNow;
            ticket.Spot.Id = request.SpotId;

            context.TicketNumbers.Update(ticket);
            await context.SaveChangesAsync();

            var response = new TicketNumberResponse(ticket.Id, ticket.Spot.Name, ticket.Ticket, ticket.TicketType.GetDisplayName());

            return Ok(new ResultViewModel<TicketNumberResponse>(response));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX04 - Falha interna no servidor"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX05 - Falha interna no servidor"));
        }

    }

    [HttpPut("v1/ticketnumbers/serviced/{ticketNumber}")]
    public async Task<IActionResult> ServicedAsync(
        [FromServices] TakeANumberDataContext context,
        [FromRoute] string ticketNumber,
        [FromBody] TicketNumberRequest request)
    {
        try
        {
            var ticket = await context.TicketNumbers.FirstOrDefaultAsync(x => x.Ticket == ticketNumber);

            if (ticket == null)
                return BadRequest(new ResultViewModel<TicketNumberResponse>("Ticket não foi encontrado"));

            if (ticket.Serviced)
                return BadRequest(new ResultViewModel<TicketNumberResponse>("Ticket já foi atendido"));

            ticket.Serviced = true;
            ticket.ServicedDate = DateTime.UtcNow;

            context.TicketNumbers.Update(ticket);
            await context.SaveChangesAsync();

            var respose = new TicketNumberResponse(ticket.Id, ticket.Ticket, ticket.Spot.Name, ticket.TicketType.GetDisplayName());

            return Ok(new ResultViewModel<TicketNumberResponse>(respose));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX06 - Falha interna no servidor"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX07 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/ticketnumbers/clearQueue")]
    public async Task<IActionResult> ClearQueue(
        [FromServices] TakeANumberDataContext context)
    {
        try
        {
            await context.Database.ExecuteSqlRawAsync(@"TRUNCATE TABLE [ticketNumber]");
            return Ok(new ResultViewModel<dynamic>(new
            {
                response = "A limpeza da fila foi finalizada com sucesso."
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX08 - Falha interna no servidor"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<TicketNumberResponse>("TNX09 - Falha interna no servidor"));

        }
    }
}
