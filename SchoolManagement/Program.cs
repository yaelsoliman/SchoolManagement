using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Helper;
using SchoolManagement.Application.IdentityModels;
using SchoolManagement.Application.Interface.Repository;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.Repository;
using SchoolManagement.Infrastructure.Seed;
using SchoolManagement.Infrastructure.Service;
using Serilog;
using Shared.Common;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
// Add services to the container.
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<ITSCService, TSCService>();
builder.Services.AddTransient<IMarksService, MarksService>();
builder.Services.AddTransient<ISubjectsService, SubjectsService>();
builder.Services.AddTransient<INoteService, NoteService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddControllers().AddJsonOptions((option) =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(o =>
 {
     o.RequireHttpsMetadata = false;
     o.SaveToken = false;
     o.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidIssuer = builder.Configuration["JWT:Issuer"],
         ValidAudience = builder.Configuration["JWT:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),

     };
 });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuberAdmin", policy => policy.RequireRole(Roles.SuberAdmin.ToString()));
    options.AddPolicy("Teacher", policy => policy.RequireRole(Roles.Teacher.ToString()));
    options.AddPolicy("Student", policy => policy.RequireRole(Roles.Student.ToString()));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerfactory = services.GetRequiredService<ILoggerProvider>();
var logger = loggerfactory.CreateLogger("app");

try
{
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


    await SeedRoles.SeedAsync(roleManager);
    await SeedUsers.SeedSuberAdminUser(userManager, roleManager);

    logger.LogInformation("Data seeded");
    logger.LogInformation("Application Started");

}
catch (Exception ex)
{
    logger.LogWarning(ex, "An error occurred while seeding Data");
}


//var config = new ConfigurationBuilder()
//               .AddJsonFile("appsettings.json")
//               .Build();
//Log.Logger = new LoggerConfiguration()
//                .ReadFrom.Configuration(config)
//                .CreateLogger();
//try
//{
//    Log.Information("Application Starting");

//}
//catch (Exception ex)
//{

//    Log.Fatal(ex, "The application failed to start!");
//}
//finally
//{
//    Log.CloseAndFlush();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
