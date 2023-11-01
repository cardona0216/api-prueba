using Microsoft.EntityFrameworkCore;
using ApiPruebas.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

//builder.Services.AddDbContext<DbapiContext>(mysqlBuilder =>
//{

//    //mysqlBuilder.UseMySql(builder.Configuration.GetConnectionString("cadenaMysql"));
//    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("cadenaMysql"));
//});
builder.Services.AddDbContext<DbapiContext>(mysqlBuilder => mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("cadenaMysql")));

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
