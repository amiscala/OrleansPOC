using Orleans;
using Orleans.Hosting;
using OrleansLedgerPoc.Entities.Grains;
using OrleansLedgerPoc.Entities.Interfaces;

await Host.CreateDefaultBuilder()
    .UseOrleans(
        builder => builder
            .UseLocalhostClustering()
            .UseTransactions()
            .AddDynamoDBGrainStorage("AccountStore", x =>
            {
                x.CreateIfNotExists = true;
                x.UpdateIfExists = true;
                x.TableName = "AccountState";
                x.UseJson = true;
                x.Service = "sa-east-1";
                x.UseProvisionedThroughput = false;
                //x.WriteCapacityUnits = 6000;
                //x.ReadCapacityUnits = 1000;
            })
            //.AddMemoryGrainStorageAsDefault()
            .ConfigureApplicationParts(parts => parts
                .AddApplicationPart(typeof(AccountGrain).Assembly)
                .AddApplicationPart(typeof(IAccountGrain).Assembly)
                .AddApplicationPart(typeof(TransferGrain).Assembly)
                .AddApplicationPart(typeof(ITransferGrain).Assembly))
            //.UseDashboard(options =>
            //{
            //    options.Port = 8888;
            //})
            )
    .ConfigureLogging(
        builder => builder
            .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
            .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning))
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.UseKestrel(opts => opts.ListenAnyIP(5000));
    })
    .RunConsoleAsync();
