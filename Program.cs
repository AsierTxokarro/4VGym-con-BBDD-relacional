using _4GYM.Data;
using _4GYM.Repositories;
using _4VGymAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Bd4VgymContext>();

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ActivityRepository>();
builder.Services.AddScoped<ActivityTypeRepository>();
builder.Services.AddScoped<InstructorRepository>();
var app = builder.Build();

// Configuración
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();