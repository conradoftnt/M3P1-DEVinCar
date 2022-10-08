using System.Text.Json.Serialization;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DevInCarDbContext>();

builder.Services.AddScoped<IAddressesRepository,AddressesRepository>();
builder.Services.AddScoped<ICarRepository,CarRepository>();
builder.Services.AddScoped<IDeliverRepository,DeliverRepository>();
builder.Services.AddScoped<ISalesRepository,SalesRepository>();
builder.Services.AddScoped<IStatesRepository,StatesRepository>();
builder.Services.AddScoped<IUsersRepository,UsersRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// comentando para conseguir trabalhar com Insomnia/Postman via http comum
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
