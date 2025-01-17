using DiscordBotDashboard.Web;
using DiscordBotDashboard.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services
    .AddCustomSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddTelemetry()
    .AddControllers();

var app = builder.Build();

app.RegisterBoxing();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseSwagger()
    .UseSwaggerUI()
    .UseCors(c =>
    {
        c.AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin()
         .WithExposedHeaders("Content-Disposition");
    })
    .UseStaticFiles()
    .UseAntiforgery();

app.MapPrometheusScrapingEndpoint();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DiscordBotDashboard.Web.Client._Imports).Assembly);

app.Run();
