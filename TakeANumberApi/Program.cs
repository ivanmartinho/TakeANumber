using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using TakeANumberApi.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureService(builder);


var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
app.UseStaticFiles();
app.MapControllers();
app.MapGet("/", () => "Ok");

app.Run();

//void LoadConfiguration(WebApplication app) { }

void ConfigureAuthentication(WebApplicationBuilder builder) { }

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<GzipCompressionProvider>();
        options.EnableForHttps = true;
    });
    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.SmallestSize;
    });

    builder
        .Services
        .AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            // Erros serão validados na Controller e retornados 
            // de forma personalizada na requisição
            options.SuppressModelStateInvalidFilter = true;
        });
}

void ConfigureService(WebApplicationBuilder builder)
{
    // Declara a connectionString.
    // Poderia ser feito tudo em uma única linha, mas para fins de depuração separado fica melhor
    var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

    // Configura a utilização do SQL Server. 
    builder.Services.AddDbContext<TakeANumberDataContext>(options =>
        options.UseSqlServer(connectionstring));
}