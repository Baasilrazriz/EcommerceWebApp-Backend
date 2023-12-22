using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ForgotService>();
builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<CreateAdmin>();
builder.Services.AddScoped<CreateRider>();
builder.Services.AddScoped<CreateSeller>();
builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<CreateCategory>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UpdateProductService>(); 

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<UpdateAdminService>();
builder.Services.AddScoped<UpdateSellerService>();
builder.Services.AddScoped<UpdateUserService>();


builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Replace with the actual URL of your React app
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // If your front-end needs to send credentials like cookies or auth headers
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=shahood-rehan;Initial Catalog=ECommerceWebApplication;Integrated Security=True;Trust Server Certificate=True")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
