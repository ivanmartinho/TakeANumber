using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeANumber.Data;
using TakeANumber.DTOs;
using TakeANumber.ViewModels;
using TakeANumber.Models;
using TakeANumber.Extensions;
using Azure.Core;
using TakeANumber.Enums;

namespace TakeANumber.Controllers;
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
            TicketGroup = new TicketGroup { Id = request.TicketGroupId },
            Spot = new Spot { Id = request.SpotId },
            Company = new Company { Id = request.CompanyId },
            TicketType = request.TicketType,
        };

        if (!TryValidateModel(model))
            return BadRequest(new ResultViewModel<TicketNumberResponse>(ModelState.GetErrros()));

        try
        {
            var ticketGroup = await context.TicketGroups.FirstOrDefaultAsync(x => x.Id == request.TicketGroupId);
            var number = await context.TicketNumbers.CountAsync();
            var ticket = new TicketNumber()
            {
                Ticket = ticketGroup.Acronym + number.ToString("D"),
                TicketGroup = ticketGroup,
                Spot = new Spot { Id = request.SpotId },
                Company = new Company { Id = request.CompanyId },
                TicketType = request.TicketType,
            };

            await context.TicketNumbers.AddAsync(ticket);
            await context.SaveChangesAsync();
            var teste = TicketType.Priority;
            var retorno = teste.ToString();
            var reponse = new TicketNumberResponse
            {
                Id = ticket.Id,
                SpotName = ticket.Spot.Name,
                TicketNumber = ticket.Ticket,
                TicketType = ticket.TicketType.GetDisplayName()
            };

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

    //[HttpPut("v1/ticketnumbers/{ticket:string}")]
    //public async Task<IActionResult> PutAsync(
    //    [FromServices] TakeANumberDataContext context,
    //    [FromRoute] string ticket)
}
