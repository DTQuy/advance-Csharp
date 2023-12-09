using advance_Csharp;
using advance_Csharp.Database;
using advance_Csharp.Service.Authorization;
using advance_Csharp.Service.Seeding;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
// Add log4net
/*XmlConfigurator.Configure(new FileInfo("log4net.config"));
*/

// configure strongly typed settings object
builder.Services.ConfigureCors();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));


WebApplication app = builder.Build();
await SeedDatabase();
// Dependency Injection:
async Task SeedDatabase()
{
    using IServiceScope scope = app.Services.CreateScope();
    AdvanceCsharpContext scopedContext = scope.ServiceProvider.GetRequiredService<AdvanceCsharpContext>();
    await DbInitializer.Initialize(scopedContext);

}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseStaticFiles();

// custom jwt auth middleware
/*app.UseMiddleware<JwtMiddleware>();
*/
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
