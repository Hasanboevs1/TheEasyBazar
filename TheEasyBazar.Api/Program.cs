using Microsoft.EntityFrameworkCore;
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



builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddAutoMapper(typeof(MappingProfile));

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
