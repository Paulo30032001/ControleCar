using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ControleCar.Data;
using ControleCar.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ControleCarContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ControleCarContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<clienteService>();
builder.Services.AddScoped<formas_pagamentoService>();
builder.Services.AddScoped<pecas_departamentosService>();
builder.Services.AddScoped<vendedorService>();
builder.Services.AddScoped<pecasService>();
builder.Services.AddScoped<vendasService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=index}/{id?}");

app.Run();
