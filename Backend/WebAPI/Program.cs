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

#region Application

builder.Services.AddScoped(typeof(IBaseApp<>), typeof(BaseApp<>));
builder.Services.AddScoped<IUsuariosApp, UsuariosApp>();
builder.Services.AddScoped<IPreRegistroApp, PreRegistroApp>();

#endregion

#region Services

builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IPreRegistroService, PreRegistroService>();

#endregion

#region Repositories

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IPreRegistroRepository, PreRegistroRepository>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
