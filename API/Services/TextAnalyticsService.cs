using Azure;
using Azure.AI.TextAnalytics;

namespace API
{
    public class TextAnalyticsService
    {
        private readonly TextAnalyticsClient _client;

        public TextAnalyticsService(string endpoint, string apiKey)
        {
            var credentials = new AzureKeyCredential(apiKey);
            _client = new TextAnalyticsClient(new Uri(endpoint), credentials);
        }

        public async Task<string> AnalyzeSentimentAsync(string message)
        {
            DocumentSentiment documentSentiment = await _client.AnalyzeSentimentAsync(message);

            return documentSentiment.Sentiment.ToString();
        }
    }
}