namespace HtmlConverter.WebApi.Models
{
    public class Root
    {
        public string to { get; set; }
        public Fetcher fetcher { get; set; }
        public Converter converter { get; set; }
        public string template { get; set; }
    }
}
