using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});
builder.Services.AddControllers();

builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<CabinetRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
