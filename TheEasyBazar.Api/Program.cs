using Microsoft.EntityFrameworkCore;
using Serilog;
using TheEasyBazar.Api.Extensions;
using TheEasyBazar.Api.MiddleWare;
using TheEasyBazar.Data;
using TheEasyBazar.Data.IRepositories;
using TheEasyBazar.Data.Repositories;
using TheEasyBazar.Service.Interfaces;
using TheEasyBazar.Service.Mappings;
using TheEasyBazar.Service.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Bazar"));
});


builder.Services.AddCustomService();

builder.Services.AddAutoMapper(typeof(MappingProfile));


// Logging

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
