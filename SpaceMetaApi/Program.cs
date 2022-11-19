using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using SpaceMetaApi.Configuration;
using SpaceMetaApi.Data;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("SpaceMetaDb");
builder.Services.AddDbContext<SpaceMetaDbContext>(opt => opt.UseSqlServer(connectionString));

// Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

// Antiforgery
builder.Services.AddAntiforgery(opt =>
{
    opt.HeaderName = "X-XSRF-TOKEN";
});

// Services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SpaceMetaApi", Version = "v1" });
    opt.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
        In = ParameterLocation.Header,
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme"
    });
});

builder.Services.AddCors(opt => opt.AddPolicy("SpaceMetaApiPolicy", policy =>
{
    policy.WithOrigins(
        "https://space-meta-dev.vercel.app",
        "https://space-meta-tsm.vercel.app",
        "https://space-meta.vercel.app"
        )
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("SpaceMetaApiPolicy");
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
