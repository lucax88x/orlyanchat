using System;
using Orleans;
using Orleans.TestingHost;

namespace OrlyanChat.Tests;

public sealed class OrleansFixture : IDisposable
{
    private readonly TestCluster cluster;

    public IGrainFactory Factory => cluster.GrainFactory;
    
    public OrleansFixture()
    {
        cluster = new TestClusterBuilder().Build();
        
        cluster.Deploy();
    }
    
    public void Dispose()
    {
        cluster.StopAllSilos();
        cluster.Dispose();
    }
}
