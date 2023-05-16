using PetProject.IdentityServer.Host.Extensions;
using PetProject.IdentityServer.Database.Extensions;
using Serilog;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.RequireHeaderSymmetry = false;
    options.ForwardLimit = null;
    options.KnownProxies.Add(IPAddress.Parse("::ffff:172.18.0.1"));
});

builder.Services.AddIdentityDatabase(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.IsDevelopment = builder.Environment.IsDevelopment();
});

builder.Host.AddSerilog(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment() is false)
    app.UseHttpsRedirection();

app.UseRouting();

//app.UseIdentityServer();

app.UseAuthorization();
app.MapControllers();

app.Run();
