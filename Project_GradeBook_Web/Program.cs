using Microsoft.EntityFrameworkCore;
using Project_GradeBook_Web.DbContext;
using Project_GradeBook_Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IMarkService, MarkService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
