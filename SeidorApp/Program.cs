using SeidorApp.Core.Adapt;
using SeidorApp.Core.Business;
using SeidorApp.Core.DataBase;
using BaseCore.DataBase.Factory;
using BaseCore.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Session", new OpenApiSecurityScheme
    {
        Description = @"Session of the current logged user.",
        Name = "Session",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Session"
                },
                Name = "Session",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


AddCoreServices(builder.Services, builder.Configuration);
AddServices(builder.Services, builder.Configuration);


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
    services.AddHttpContextAccessor();
    services.AddCors(x => x.AddDefaultPolicy(s => s.SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyOrigin().AllowAnyMethod().
                           AllowAnyHeader().DisallowCredentials().
                           SetIsOriginAllowed((host) => true)));

    SqliteDataBaseIntegrityService.ValidateIntegrityAndBuildDB(configuration.GetValue<string>("SQLiteFilePath"), configuration.GetConnectionString("Default"));
    services.AddScoped<IDBConnectionFactory, SQLiteConnectionFactory>(x => new SQLiteConnectionFactory(configuration.GetConnectionString("Default")));
    services.AddScoped<IAuth, SessionBusiness>();

}

void AddServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped<UserBusiness>();
    services.AddScoped<CpfBusiness>();
    services.AddScoped<UserAdapter>();
}