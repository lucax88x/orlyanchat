using Orleans;
using OrlyanChat.Web.Api.Services;
using OrlyanChat.Web.Api.Services.Rng;
using OrlyanChat.Web.Api.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<OrleansClientHostedService>();

builder.Services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<OrleansClientHostedService>());
builder.Services.AddSingleton(sp => sp.GetRequiredService<OrleansClientHostedService>().Client);
builder.Services.AddSingleton<IGrainFactory>(sp => sp.GetRequiredService<OrleansClientHostedService>().Client);

builder.Services.AddMvc();

builder.Services.AddTransient<IRngService, RngService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

app.MapControllers();

await app.RunAsync();
