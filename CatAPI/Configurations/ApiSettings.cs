namespace CatApi.Configurations
{
    public class ApiSettings
    {
            public string[] AllowedOrigins { get; set; }
            public string? CatFactApiUrl { get; set; }
            public string? CatImageBaseUrl { get; set; }
            public string? Port { get; set; }
            public string? FrontendUrl { get; set; }
    }
}
