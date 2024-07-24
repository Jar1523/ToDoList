using Microsoft.EntityFrameworkCore;
using ToDoList;

var builder = WebApplication.CreateBuilder(args);

var Allow = "AllowOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(Allow, options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ListContext>(
    options => options.UseSqlServer("Server=localhost,1433;Database=TestDBLocal;Trusted_Connection=False;TrustServerCertificate=True;Encrypt=True;User Id=sa;Password=Password123")
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(Allow);

app.UseAuthorization();

app.MapControllers();

app.Run();
