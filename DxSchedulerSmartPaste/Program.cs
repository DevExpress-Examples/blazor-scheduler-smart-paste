using Azure;
using Azure.AI.OpenAI;
using DxSchedulerSmartPaste.Components;
using DxSchedulerSmartPaste.Services;
using Microsoft.Extensions.AI;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDevExpressBlazor(options =>
{
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.Services.AddMvc();

// Demo AI services are rate limited and intended for demonstration purposes only.
// DevExpress does not offer a REST API and does not ship any built-in LLMs/SLMs.
// Use of demo credentials in production is strictly prohibited.
// Specify the Azure OpenAI endpoint, key, and deployment name in the 'appsettings.json' file.
var openAiServiceSettings = builder.Configuration.GetSection("AzureOpenAISettings").Get<AzureOpenAIServiceSettings>();
if (openAiServiceSettings == null ||
    string.IsNullOrEmpty(openAiServiceSettings.Endpoint) ||
    string.IsNullOrEmpty(openAiServiceSettings.Key) ||
    string.IsNullOrEmpty(openAiServiceSettings.DeploymentName))
    throw new InvalidOperationException("Specify the Azure OpenAI endpoint, key, and deployment name in the 'appsettings.json' file.");
var chatClient = new AzureOpenAIClient(
     new Uri(openAiServiceSettings.Endpoint),
     new AzureKeyCredential(openAiServiceSettings.Key))
    .GetChatClient(openAiServiceSettings.DeploymentName)
    .AsIChatClient();

builder.Services.AddSingleton(chatClient);
builder.Services.AddDevExpressAI();

var app = builder.Build();

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
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();
