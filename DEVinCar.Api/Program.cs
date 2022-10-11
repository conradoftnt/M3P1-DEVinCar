using System.Text.Json.Serialization;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Infra.Data.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Services;
using DEVinCar.Api.Config;
using DEVinCar.Api.Security;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DEVinCar.Domain.Models;

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
builder.Services.AddScoped<CacheService<Car>>();
builder.Services.AddScoped<ILoginRepository,LoginRepository>();
builder.Services.AddScoped<ILoginService,LoginService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddMvc( config => {
    config.ReturnHttpNotAcceptable = true;
    config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    config.InputFormatters.Add(new XmlSerializerInputFormatter(config));
});

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
app.UseMiddleware<ErrorMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
