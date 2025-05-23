using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OdontoGuardAPI.Data;
using OdontoGuardAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddControllers();

// Configuração de autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            // Verificar se a chave não é nula antes de usar
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetJwtKey()))
        };
    });


builder.Services.AddDbContext<OdontoGuardDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OdontoGuard API", Version = "v1" });
});


builder.Services.AddSingleton<SentimentAnalysisService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdontoGuard API v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


string GetJwtKey()
{
    var key = builder.Configuration["Jwt:Key"];
    if (string.IsNullOrEmpty(key))
    {
        throw new InvalidOperationException("JWT key is missing.");
    }
    return key;
}
