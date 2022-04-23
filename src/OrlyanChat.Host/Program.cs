using System.Net;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrlyanChat.Grains;

using var host = Host.CreateDefaultBuilder(args).UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering().Configure<ClusterOptions>(opts =>
    {
        opts.ClusterId = "local-orlyanchat-cluster";
        opts.ServiceId = "orlyanchat";
    }).Configure<EndpointOptions>(opts =>
    {
        opts.GatewayPort = 30000;
        opts.SiloPort = 11111;
        opts.AdvertisedIPAddress = IPAddress.Loopback;
    }).ConfigureApplicationParts(partman => { partman.AddApplicationPart(typeof(RngGrain).Assembly); });
}).Build();

await host.RunAsync();
