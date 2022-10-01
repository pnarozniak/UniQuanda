using MediatR;
using UniQuanda.Infrastructure;
using UniQuanda.Infrastructure.Presistence;
using UniQuanda.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructurePersistence(builder.Configuration);

// Configure CORS
builder.Services.AddCORS(builder.Configuration);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();