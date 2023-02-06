using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(
    o => o.UseQueryTrackingBehavior
    (
        QueryTrackingBehavior.NoTracking
    )
);
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.MapGet("/applications", (IApplicationRepository repository) => repository.GetAllApplications())
.Produces<ApplicationDto[]>(StatusCodes.Status200OK);

app.MapGet("/application/{id:int}", (IApplicationRepository repository, int id) => {
    var application = repository.GetApplication(id);
    if(application == null)
    {
        return Results.Problem($"Application with Id {id} not found.", statusCode: 404);
    }

    return Results.Ok(application);
}).ProducesProblem(404).Produces<ApplicationDto>(StatusCodes.Status200OK);

app.MapPost("/applications", async ([FromBody]ApplicationDto dto, IApplicationRepository repository) => {
    var newApplication = repository.AddApplication(dto);
    return Results.Created($"/applications", newApplication);
}).Produces<ApplicationDto>(StatusCodes.Status201Created);

app.Run();
