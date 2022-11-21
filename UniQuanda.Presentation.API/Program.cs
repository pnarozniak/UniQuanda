using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UniQuanda.Infrastructure;
using UniQuanda.Infrastructure.Presistence;
using UniQuanda.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UniQuanda dev API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
            },
            new string[] { }
        }
    });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(x => (x.GetName().Name?.StartsWith("UniQuanda.Core") ?? false)
    || (x.GetName().Name == "UniQuanda.Presentation.API")).ToList();
    foreach (var assembly in assemblies)
    {
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));
    }
});

//Fix DateTime issue
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructurePersistence(builder.Configuration);

// Configure CORS
builder.Services.AddCORS(builder.Configuration);

// Configure Recaptcha
builder.Services.AddRecaptcha(builder.Configuration);

// Configure authentication
builder.Services.AddJwtBearerAuth(builder.Configuration);

// Configure MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCORS();

app.UseRecaptcha();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();