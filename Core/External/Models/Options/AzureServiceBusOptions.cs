namespace Core.External.Models.Options;

public class AzureServiceBusOptions
{
    public string ConnectionString { get; set; } = null!;
    public string QueueName { get; set; } = null!;
}
