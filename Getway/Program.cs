using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Getway.Services;


var builder = WebApplication.CreateBuilder(args);

// =======================
// 🔧 SERVICES (Build Öncesi)
// =======================

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient (Gateway motoru)
builder.Services.AddHttpClient();

// Gateway Forwarder
builder.Services.AddScoped<GatewayForwarder>();

// 🔐 JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
    });


// 🔑 USER POLICIES
builder.Services.AddAuthorization(options =>
{
    // Token varsa yeterli
    options.AddPolicy("User", policy =>
        policy.RequireAuthenticatedUser());

    // Admin rolü
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    // Order yazma yetkisi
    options.AddPolicy("OrderWrite", policy =>
        policy.RequireClaim("scope", "order.write"));
});

builder.Services.AddAuthorization();

var app = builder.Build();

// =======================
// 🚦 PIPELINE (Build Sonrası)
// =======================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔐 JWT Middleware (SIRA ÇOK ÖNEMLİ)
app.UseAuthentication();
app.UseAuthorization();

// =======================
// 🔓 AUTH / LOGIN (JWT YOK)
// =======================

app.Map("/auth/{**path}", async (
    HttpContext context,
    string path,
    GatewayForwarder forwarder) =>
{
    var response = await forwarder.ForwardAsync(context, "auth", path);

    context.Response.StatusCode = (int)response.StatusCode;

    foreach (var header in response.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();

    foreach (var header in response.Content.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();

    context.Response.Headers.Remove("transfer-encoding");

    await response.Content.CopyToAsync(context.Response.Body);
})
.AllowAnonymous(); // 🔓 LOGIN SERBEST

// =======================
// 🔐 DİĞER TÜM SERVİSLER (JWT ZORUNLU)
// =======================

app.Map("/{service}/{**path}", async (
    HttpContext context,
    string service,
    string path,
    GatewayForwarder forwarder) =>
{
    var response = await forwarder.ForwardAsync(context, service, path);

    context.Response.StatusCode = (int)response.StatusCode;

    foreach (var header in response.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();

    foreach (var header in response.Content.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();

    context.Response.Headers.Remove("transfer-encoding");

    await response.Content.CopyToAsync(context.Response.Body);
})
.RequireAuthorization(); // 🔐 JWT ŞART

app.Run();
