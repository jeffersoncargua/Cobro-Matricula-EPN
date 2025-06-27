using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Mapping;
using Cobro_Matricula_EPN.Repository;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility;


var builder = WebApplication.CreateBuilder(args);

//Add provider and configuration to allow CORS configuration
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();



//Add services for CORS configuration
builder.Services.AddCors(options =>
{
    var frontEndUrl = configuration.GetValue<string>("FrontEndConfiguration:Url");

    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontEndUrl)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});


// Add services to the container.

builder.Services.AddControllers();

//Add Connection SQL Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Add Identity Services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
    AddEntityFrameworkStores<ApplicationDbContext>().
    AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
});


//Add Repositories Services
//builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBaseParameterRepository, BaseParameterRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

//Add Service AutoMapper 
builder.Services.AddAutoMapper(typeof(MappingConfig));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Services Send Email Configuration
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

//Add Services Connection Front-End 
var frontUrl = builder.Configuration.GetSection("FrontEndConfiguration").Get<FrontEndConfig>();
builder.Services.AddSingleton(frontUrl);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaCobro");
    });
}



app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
