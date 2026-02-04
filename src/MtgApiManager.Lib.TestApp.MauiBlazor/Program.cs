using MtgApiManager.Lib.TestApp.MauiBlazor.Components;
using MudBlazor.Services;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.TestApp.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add MTG services
builder.Services.AddSingleton<IMtgServiceProvider>(new MtgServiceProvider());
builder.Services.AddSingleton<MtgController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
