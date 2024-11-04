using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using System.Text.Json;

using TodoApi.Infrastructure;
using TodoApi.Services.User;
using TodoApi.Services.Login;
using TodoApi.Models.Shared;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Services;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Staff;
using TodoApi.Infrastructure.OperationRequestLog;
using TodoApi.Services.OperationType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string domain = builder.Configuration["Auth0:Domain"];
string audience = builder.Configuration["Auth0:Audience"];
string clientId = builder.Configuration["Auth0:ClientId"];
string clientSecret = builder.Configuration["Auth0:ClientSecret"];



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://{domain}";
    options.Audience = audience;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://{domain}",

        ValidateAudience = true,
        ValidAudience = audience, // Add both API and Client ID

        ValidateLifetime = true,

        // Automatically retrieve the signing keys from Auth0
        IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) =>
        {
            var client = new HttpClient();
            var jwks = client.GetStringAsync($"https://{domain}/.well-known/jwks.json").Result;
            return new JsonWebKeySet(jwks).GetSigningKeys();
        }
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == Auth0Data.ROLES_URL && (c.Value == "Admin")) &&
            context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"));
    });
    options.AddPolicy("DoctorPolicy", policy =>
    {
    policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == Auth0Data.ROLES_URL && (c.Value == "Doctor")) &&
            context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"));
    });
    options.AddPolicy("TechnicianPolicy", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == Auth0Data.ROLES_URL && (c.Value == "Technician")) &&
            context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"));
    });
    options.AddPolicy("NursePolicy", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == Auth0Data.ROLES_URL && (c.Value == "Nurse")) &&
            context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"));
    });
    options.AddPolicy("PatientPolicy", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == Auth0Data.ROLES_URL && (c.Value == "Patient")) &&
            context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"));
    });
    options.AddPolicy("BackOfficeUserPolicy", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == Auth0Data.ROLES_URL && 
                                        (c.Value == "Admin" || c.Value == "Doctor" || c.Value == "Technician" || c.Value == "Nurse")) &&
            context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"));
    });
});

builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your desired timeout
        options.Cookie.HttpOnly = true; // Optional: make the session cookie HTTP-only
        options.Cookie.IsEssential = true; // Required for session to be available during consent
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IPOContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOperationRequestRepository, OperationRequestRepository>();
builder.Services.AddScoped<IOperationRequestService, OperationRequestService>();
builder.Services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
builder.Services.AddScoped<IOperationTypeService, OperationTypeService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IOperationRequestLogRepository, OperationRequestLogRepository>();


// Register UserService with HttpClient
builder.Services.AddHttpClient<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


