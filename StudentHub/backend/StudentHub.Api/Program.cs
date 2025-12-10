using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Api.Models.Annoucements;
using StudentHub.Api.Services;
using StudentHub.Api.Services.Announcements;
using StudentHub.Api.Services.Shedule;
using StudentHub.Application.Services.Tasks;
using StudentHub.Core.Entities.Identity;
using StudentHub.Infrastructure.Data;
using StudentHub.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Services CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
});
// DB
builder.Services.AddDbContext<StudentHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<StudentHubDbContext>()
    .AddDefaultTokenProviders();

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    var key = Encoding.UTF8.GetBytes(jwtKey);

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});
// App services
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowReact");
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// Seed data
await SeedData.InitializeAsync(app);
app.Run();