using Google.Protobuf.WellKnownTypes;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var apiService = builder.AddProject<Projects.ClinicAspire_ApiService>("apiservice");

        builder.AddProject<Projects.ClinicAspire_Web>("webfrontend")
            .WithExternalHttpEndpoints()
            .WithReference(apiService)
            .WaitFor(apiService);

        builder.Build().Run();
    }
}