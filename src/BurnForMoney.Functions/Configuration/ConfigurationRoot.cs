namespace BurnForMoney.Functions.Configuration
{
    public class ConfigurationRoot
    {
        public EmailSection Email { get; set; }
        public ConnectionStringsSection ConnectionStrings { get; set; }
        public bool IsLocalEnvironment { get; set; }
        public string ApplicationInsightsInstrumentationKey { get; set; }
        public string SendGridApiKey { get; set; }
        public EventGridSection EventGrid { get; set; }

        public bool IsValid()
        {
            return  ConnectionStrings != null;
        }
    }

    public class EventGridSection
    {
        public string SasKey { get; set; }
        public string TopicEndpoint { get; set; }
    }

    public class EmailSection
    {
        public string SenderEmail { get; set; }
        public string ReportsReceiver { get; set; }
        public string DefaultRecipient { get; set; }
    }

    public class ConnectionStringsSection
    {
        public string SqlDbConnectionString { get; set; }
        public string AzureWebJobsStorage { get; set; }
    }

    public class StravaConfigurationSection
    {
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessTokensEncryptionKey { get; set; }
    }
}