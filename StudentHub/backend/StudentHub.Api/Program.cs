using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Api.Hubs;
using StudentHub.Api.Services;
using StudentHub.Api.Services.Announcements;
using StudentHub.Api.Services.Chat;
using StudentHub.Api.Services.Groups;
using StudentHub.Api.Services.Shedule;
using StudentHub.API.Services.Courses;
using StudentHub.API.Services.Groups;
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
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // Allow passing token in SignalR WebSocket URL
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/chat-hub"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});
// App services
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddSignalR();
// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Build app
var app = builder.Build();

app.UseCors("AllowReact");
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");
// Seed data
await SeedData.InitializeAsync(app);
// Run app
app.Run();