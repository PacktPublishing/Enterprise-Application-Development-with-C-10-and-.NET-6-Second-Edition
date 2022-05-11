namespace TestConfiguration
{
    public class ApiConfig
    {
        public string Name { get; set; }

        public string BaseUri { get; set; }

        public int HttpTimeOutInSeconds { get; set; }

        public List<ApiUrl> ApiUrls { get; set; }
    }

}
