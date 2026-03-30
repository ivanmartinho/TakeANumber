using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TakeANumberApi.Data;
using TakeANumberApi.Extensions;
using TakeANumberApi.Models;
using TakeANumberShared.ViewModels;

namespace TakeANumberApi.Controllers
{
    [ApiController]
    public class SpotController : ControllerBase
    {
        [HttpGet("v1/spots")]
        public async Task<IActionResult> GetAsync(
            [FromServices] TakeANumberDataContext context,
            [FromRoute] int page = 0,
            [FromRoute] int pageSize = 25)
        {
            try
            {
                var total = await context.Spots.AsNoTracking().CountAsync();
                var spots = await context
                    .Spots
                    .AsNoTracking()
                    .Select(x=>new ListSpotsViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
                var data = new PagedViewModel<List<ListSpotsViewModel>>()
                {
                    Data = spots,
                    Page = page,
                    PageSize = pageSize,
                    Total = total
                };

                return Ok(new ResultViewModel<PagedViewModel<List<ListSpotsViewModel>>> (data));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<Spot>>("SX01 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/spots/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] TakeANumberDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var spot = await context.Spots.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (spot == null)
                    return NotFound(new ResultViewModel<Spot>("Local não foi localizado."));

                return Ok(new ResultViewModel<Spot>(spot));
            }
            catch (Exception)
            {
                return StatusCode(500, "SX02 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/spots/{id:int}")]
        public async Task<IActionResult> PostAsync(
            [FromServices] TakeANumberDataContext context,
            [FromBody] EditorSpotViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Spot>(ModelState.GetErrros()));

            try
            {
                var spot = new Spot()
                {
                    Name = model.Name
                };

                await context.Spots.AddAsync(spot);
                await context.SaveChangesAsync();
                return Created($"v1/spots/{spot.Id}", new ResultViewModel<Spot>(spot));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Spot>("SX03 - Falha interna no servidor"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Spot>("SX04 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/spots/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices]TakeANumberDataContext context,
            [FromRoute]int id,
            [FromBody] EditorSpotViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Spot>(ModelState.GetErrros()));

            var spot = await context.Spots.FirstOrDefaultAsync(x => x.Id == id);
            if (spot == null)
                NotFound(new ResultViewModel<Spot>("Local não foi localizado"));

            try
            {
                spot.Name = model.Name;

                context.Spots.Update(spot);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Spot>(spot));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "SX05 - Falha interna no servidor");
            }
            catch (Exception)
            {
                return StatusCode(500, "SX06 - Falha interna no servidor");
            }
        }

        [HttpDelete("v1/spots/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] TakeANumberDataContext context,
            [FromRoute]int id)
        {
            var spot = await context.Spots.FirstOrDefaultAsync(x => x.Id == id);

            if (spot == null)
                return NotFound("Local não foi localizado");

            try
            {
                context.Spots.Remove(spot);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Spot>(spot));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "SX07 - Falha interna no servidor");
            }
            catch(Exception)
            {
                return StatusCode(500, "SX08 - Falha interna no servidor");
            }
        }
    }
}
