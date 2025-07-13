using Finiti.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Finiti.WEB.Middleware;
using Finiti.DOMAIN.Repositories;
using Finiti.DATA.Repositories;
using Finiti.DOMAIN.Services;
using Finiti.APPLICATION.Services;
using Finiti.WEB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));



builder.Services.AddScoped<IAuthorRepository,AuthorRepository>();
builder.Services.AddScoped<IAuthService,AuthService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest"))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwtToken"];
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql("Server=localhost;Port=5432;Database=finiti;User Id=finiti;Password=admin;");
}, ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ClaimsMiddleware>();
app.MapControllers();


app.Run();
