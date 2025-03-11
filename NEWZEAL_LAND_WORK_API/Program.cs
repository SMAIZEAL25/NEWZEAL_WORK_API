using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.MapConfig;
using NEWZEAL_LAND_WORK_API.Middleware;
using NEWZEAL_LAND_WORK_API.Repositories;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container 

var Logger = new LoggerConfiguration().WriteTo.Console().MinimumLevel.Warning().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Logger);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Implementation of Authorization with jwt token into swagger for this projects
builder.Services.AddSwaggerGen( Options =>
{
    Options.SwaggerDoc("v1", new() { Title = "NEWZEAL_LAND_WORK_API", Version = "v1" });
    Options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });

    Options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme,
                },

                Scheme = "OAuth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            //Array.Empty<string>()
            new List<string> ()
        }
});
});

//Application DB
builder.Services.AddDbContext<NZwalksDbcontext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("NzWalksConnectionStrings")));


// Authentication DB
builder.Services.AddDbContext<NzwalksAuthDBContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("NzWalksAuthConnectionStrings")));

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IRepositoriesNZwalks, RepositoriesNZwalksass>();
builder.Services.AddScoped<IWalksRepository, WalksRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Nzwalks")
    .AddEntityFrameworkStores<NzwalksAuthDBContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;
    //options.User.RequireUniqueEmail = true;
    //options.SignIn.RequireConfirmedEmail = true;
    //options.Tokens.EmailConfirmationTokenProvider = "Nzwalks";
    //options.Tokens.ChangeEmailToken Provider = "Nzwalks";
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
                                                                                                                        //ValidIssuer = "https://dev-7z1v7v7z.us.auth0.com/",
                                                                                                                        //ValidAudience = "https://NZwalks",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
    options.Authority = "https://dev-7z1v7v7z.us.auth0.com/";
    options.Audience = "https://NZwalks";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseMiddleware<ExceptionHandlerMiddleWare>();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UploadimagesFloder")),

    RequestPath = "/UploadimagesFloder"
});

app.MapControllers();
app.Run();
 