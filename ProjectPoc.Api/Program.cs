using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using ProjectPoc.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Simple JWT settings (for demo). In production move to configuration and secure secrets.
var jwtKey = "super_secret_key_123!"; // must be at least 16 chars for HMACSHA256
var jwtIssuer = "ProjectPoc";
var jwtAudience = "ProjectPocUsers";

// Add services to the container.
builder.Services.AddControllers();

// Configure EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ProjectPocDb"));

// MediatR - register handlers from current assembly (MediatR v11)
builder.Services.AddMediatR(typeof(Program).Assembly);

// Register FluentValidation validators from assembly
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configure authentication with JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Authorization policies (by role and by claim)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("HasDepartment", policy => policy.RequireClaim("department"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Seed some data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.TodoItems.Add(new TodoItem { Title = "Initial item", IsCompleted = false });
    db.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
