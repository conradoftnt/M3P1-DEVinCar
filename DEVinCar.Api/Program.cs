using System.Text.Json.Serialization;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Infra.Data.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DevInCarDbContext>();

builder.Services.AddScoped<IAddressesRepository,AddressesRepository>();
builder.Services.AddScoped<IAddressesService,AddressesService>();
builder.Services.AddScoped<ICarRepository,CarRepository>();
builder.Services.AddScoped<ICarsService,CarsService>();
builder.Services.AddScoped<IDeliverRepository,DeliverRepository>();
builder.Services.AddScoped<IDeliverService,DeliverService>();
builder.Services.AddScoped<ISalesRepository,SalesRepository>();
builder.Services.AddScoped<ISalesService,SalesService>();
builder.Services.AddScoped<IStatesRepository,StatesRepository>();
builder.Services.AddScoped<IStatesService,StatesService>();
builder.Services.AddScoped<IUsersRepository,UsersRepository>();
builder.Services.AddScoped<IUsersService,UsersService>();

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
