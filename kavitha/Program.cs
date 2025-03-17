using kavitha.Data;
using kavitha.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<JwtHelper>();

// ✅ Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Enable Controllers
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowSpecificOrigin",
    builder => builder.AllowAnyOrigin() // Replace with your frontend domain
                      .AllowAnyHeader()
                      .AllowAnyMethod());
});
// ✅ Add Swagger (for API documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");

// ✅ Enable Swagger UI
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
// Add Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//      options.TokenValidationParameters = new TokenValidationParameters
//      {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//      };
//});


     app.Run();
//    });

//using kavitha.Data;
//using kavitha.Helpers;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Add Database Context
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ✅ Add JWT Helper
//builder.Services.AddSingleton<JwtHelper>();

//// ✅ Enable Controllers
//builder.Services.AddControllers();

//// ✅ Enable CORS
//builder.Services.AddCors(options =>
//{
//  options.AddPolicy("AllowSpecificOrigin",
//      policy => policy.AllowAnyOrigin()
//                      .AllowAnyHeader()
//                      .AllowAnyMethod());
//});

//// ✅ Add Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//      options.TokenValidationParameters = new TokenValidationParameters
//      {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//      };
//    });

//// ✅ Add Authorization
//builder.Services.AddAuthorization();

//// ✅ Add Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Travel API", Version = "v1" });
//  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//  {
//    In = ParameterLocation.Header,
//    Description = "Enter JWT token",
//    Name = "Authorization",
//    Type = SecuritySchemeType.Http,
//    Scheme = "Bearer"
//  });
//  c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
//            },
//            Array.Empty<string>()
//        }
//    });
//});

//var app = builder.Build();

//app.UseHttpsRedirection();

//// ✅ Enable CORS
//app.UseCors("AllowSpecificOrigin");

//// ✅ Enable Authentication & Authorization (correct order)
//app.UseAuthentication();
//app.UseAuthorization();

//// ✅ Enable Swagger UI (Only in Development)
//if (app.Environment.IsDevelopment())
//{
//  app.UseSwagger();
//  app.UseSwaggerUI();
//}

//// ✅ Map Controllers
//app.MapControllers();

//app.Run();


