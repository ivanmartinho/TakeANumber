using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeANumber.Data;
using TakeANumber.Extensions;
using TakeANumber.Models;
using TakeANumber.ViewModels;

namespace TakeANumber.Controllers
{
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet("v1/companies")]
        public async Task<IActionResult> GetAsync(
            [FromServices] TakeANumberDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Companies.AsNoTracking().CountAsync();
                var companies = await context
                    .Companies
                    .AsNoTracking()
                    .Select(x=>new ListCompaniesViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
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
                    data = companies
                }));
            }
            catch (Exception)
            {
                // A sigla CX01 serve para indicar onde eu preciso verificar o código. Desta forma não retorno
                // dados que não são necessários para quem está consumindo a API, deixando seu uso mais seguro.
                return StatusCode(500, new ResultViewModel<List<Company>>("CX01 - Falha interna no servidor."));
            }
        }

        [HttpGet("v1/companies/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] TakeANumberDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var company = await context.Companies.FirstOrDefaultAsync(x => x.Id == id);
                if (company == null)
                    return NotFound(new ResultViewModel<Company>("Empresa não foi localizada"));

                return Ok(new ResultViewModel<Company>(company));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX02 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/companies")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCompanyViewModel model,
            [FromServices] TakeANumberDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Company>(ModelState.GetErrros()));
            try
            {
                var company = new Company() { Name = model.Name };

                await context.Companies.AddAsync(company);
                await context.SaveChangesAsync();
                return Created($"v1/companies/{company.Id}", new ResultViewModel<Company>(company));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX03 - Não foi possível incluir a empresa"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX04 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/companies/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] TakeANumberDataContext context,
            [FromRoute] int id,
            [FromBody] EditorCompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Company>(ModelState.GetErrros()));

            var company = await context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (company == null)
                return NotFound(new ResultViewModel<Company>("Empresa não foi localizada."));

            try
            {
                company.Name = model.Name;

                context.Companies.Update(company);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Company>(company));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX05 - Não foi possível atualizar a empresa"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX05 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/companies/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] TakeANumberDataContext context,
            [FromRoute] int id)
        {
            var company = await context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (company == null)
                return NotFound(new ResultViewModel<Company>("A empresa não foi localizada"));

            try
            {
                context.Companies.Remove(company);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Company>(company));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX06 - Falha interna no servidor"));
            }
            catch(Exception)
            {
                return StatusCode(500, new ResultViewModel<Company>("CX07 - Falha interna no servidor."));
            }
        }

    }
}
