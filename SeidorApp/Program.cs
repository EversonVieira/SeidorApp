using SeidorApp.Core.DataBase;
using SeidorCore.DataBase.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddCoreServices(builder.Services, builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void AddCoreServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddLogging();
    services.AddCors(x => x.AddDefaultPolicy(s => s.SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyOrigin().AllowAnyMethod().
                           AllowAnyHeader().DisallowCredentials().
                           SetIsOriginAllowed((host) => true)));

    SqliteDataBaseIntegrityService.ValidateIntegrityAndBuildDB(configuration.GetValue<string>("SQLiteFilePath"), configuration.GetConnectionString("Default"));
    services.AddSingleton<IDBConnectionFactory, SQLiteConnectionFactory>(x => new SQLiteConnectionFactory(configuration.GetConnectionString("Default")));

}