using Auth0.AspNetCore.Authentication;
using Blazored.Toast;
using Microsoft.FeatureManagement;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

var isDevelopment = builder.Environment.IsDevelopment();

if (!isDevelopment)
{
    string connectionStringAppConfig = builder.Configuration.GetConnectionString("AppConfig");
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(connectionStringAppConfig);
        options.UseFeatureFlags();
    });
}

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.Scope = "openid profile email";
});

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Services.AddApplicationInsightsTelemetry();
//builder.Logging.AddJsonConsole();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddBlazoredToast();
builder.Services.AddSyncfusionBlazor();
//builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));
builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseAzureAppConfiguration();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();