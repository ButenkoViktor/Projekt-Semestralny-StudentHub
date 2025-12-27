using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Api.Hubs;
using StudentHub.Api.Services;
using StudentHub.Api.Services.Announcements;
using StudentHub.Api.Services.Chat;
using StudentHub.Api.Services.Groups;
using StudentHub.Api.Services.Notifications;
using StudentHub.Api.Services.Schedule;
using StudentHub.API.Services.Courses;
using StudentHub.API.Services.Groups;
using StudentHub.Api.Services.Tasks;
using StudentHub.Api.Services.Files;
using StudentHub.Api.Services.Events;
using StudentHub.Api.Services.Notes;
using StudentHub.Core.Entities.Identity;
using StudentHub.Infrastructure.Data;
using StudentHub.Infrastructure.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//  DB 
string? conn = builder.Configuration.GetConnectionString("DefaultConnection");


if (!DatabaseExists(conn))
{
    Console.WriteLine("⚠SQLEXPRESS not found.");

    conn = "Server=(localdb)\\MSSQLLocalDB;Database=StudentHubDb;Trusted_Connection=True;";
}

builder.Services.AddDbContext<StudentHubDbContext>(options =>
    options.UseSqlServer(conn, sql =>
    {
        sql.EnableRetryOnFailure(); 
    })
);

//  CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//  IDENTITY

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<StudentHubDbContext>()
.AddDefaultTokenProviders();

//  JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // SignalR
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/chat-hub") ||
                 path.StartsWithSegments("/hubs/presence") ||
                 path.StartsWithSegments("/notifications-hub")))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

//  SERVICES

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IFileService, FileService>(); 
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ITeacherGroupService, TeacherGroupService>();

builder.Services.AddSignalR();


//  MVC + SWAGGER

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//  APP

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");
app.MapHub<NotificationHub>("/notifications-hub");
app.MapHub<PresenceHub>("/hubs/presence");


//  SEED DATA

await SeedData.InitializeAsync(app);

app.Run();

static bool DatabaseExists(string conn)
{
    try
    {
        using var testConn = new Microsoft.Data.SqlClient.SqlConnection(conn);
        testConn.Open();
        return true;
    }
    catch
    {
        return false;
    }
}
