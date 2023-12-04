using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using SRE.Program.WebAPI.BusinessLogics.Caches;
using SRE.Program.WebAPI.BusinessLogics.Common;
using SRE.Program.WebAPI.BusinessLogics.Logistics.Services;
using SRE.Program.WebAPI.DataAccess.Models;
using SRE.Program.WebAPI.DataAccess.Repositories.Logistics;
using SRE.Program.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ¨ú settings.json ³]©w
builder.Services.Configure<PostgresSettings>(
    builder.Configuration.GetSection("PostgresSettings")
);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.ListenAnyIP(5000);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PosgresDbContext>(options =>
{
    options.UseNpgsql(DatabaseConnectionExtension.GetConnectionString(builder.Configuration));
}, ServiceLifetime.Transient);

builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddScoped<ILogisticService, LogisticService>();
builder.Services.AddScoped<ILogisticRepository, LogisticRepository>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
