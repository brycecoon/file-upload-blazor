global using web.Services;
global using web.Models;

using Microsoft.AspNetCore.SignalR;
using web.Components;
using web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024 * 200; // 200MB or use null
});

builder.Services.AddSingleton<UserService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.WebHost.ConfigureAppConfiguration((ctx, cb) =>
{
    if (!ctx.HostingEnvironment.IsDevelopment())
    {
        StaticWebAssetsLoader.UseStaticWebAssets(
      ctx.HostingEnvironment,
      ctx.Configuration);
    }
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
