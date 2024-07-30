using API.Models;

namespace API.Dto
{
    public class AnalyzedMessageDto
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Sentiment { get; set; }
    }
}