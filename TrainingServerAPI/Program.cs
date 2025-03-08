using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy to allow specific origins with credentials
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:8081", "http://172.16.3.197", "http://10.0.2.2:8081")  // Add your specific origins here
               .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
               .AllowAnyHeader()  // Allow any headers
               .AllowCredentials(); // Allow credentials (cookies, headers, etc.)
    });
});

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetValue<string>("SecretKey");
        var key = Encoding.ASCII.GetBytes(secretKey);  // Convert the secret key into bytes.

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
            ValidAudience = jwtSettings.GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(key),  // Using the secret key to sign JWT tokens
            ClockSkew = TimeSpan.Zero  // Optional: Removes default clock skew to validate expiring tokens exactly at expiration time
        };
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS with the newly added policy (allow specific origins)
app.UseCors("AllowSpecificOrigins");

// Enable Authentication & Authorization
app.UseAuthentication(); // This needs to be added for JWT to work
app.UseAuthorization();

app.MapControllers();

app.Run();
