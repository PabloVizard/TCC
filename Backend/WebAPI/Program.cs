using Application.Applications.Interfaces;
using Application.Applications;
using Domain.Services.Interfaces;
using Domain.Services;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Utils;
using Infrastructure.Data.Repositories;
using Domain.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region db 

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseMySql(mySqlConnection, new MySqlServerVersion(
        new Version()
        )
    )
);

#endregion


InjectorDependencies.Registrer(builder.Services);


builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
