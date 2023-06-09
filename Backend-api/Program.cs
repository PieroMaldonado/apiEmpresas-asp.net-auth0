﻿using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Add authentication configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-0giiekio4xgysadj.us.auth0.com/";
        options.Audience = "https://crudempresasapi.azurewebsites.net/";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WriteAccess", policy =>
         policy.RequireClaim("permissions", "create:centrocostos", "create:movimientoplanilla", "update:centrocostos", "update:movimientoplanilla"));
    options.AddPolicy("ReadAccess", policy =>
         policy.RequireClaim("permissions", "read:centrocostos", "read:movimientoplanilla", "search:centrocostos", "search:movimientoplanilla"));
    options.AddPolicy("DeleteAccess", policy =>
         policy.RequireClaim("permissions", "delete:centrocostos", "delete:movimientoplanilla"));
});

// Add Swagger/OpenAPI configuration
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

// Enable authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
