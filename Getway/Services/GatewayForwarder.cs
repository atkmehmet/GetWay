namespace Getway.Services
{
    using System.Net.Http.Headers;

    public class GatewayForwarder
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GatewayForwarder(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> ForwardAsync(
            HttpContext context,
            string service,
            string path)
        {
            var baseUrl = _configuration[$"Services:{service}"];

            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception($"Service '{service}' not found.");

            var targetUri = $"{baseUrl}/{path}{context.Request.QueryString}";

            var requestMessage = new HttpRequestMessage(
                new HttpMethod(context.Request.Method),
                targetUri
            );

            // Body kopyala
            if (context.Request.ContentLength > 0)
                requestMessage.Content = new StreamContent(context.Request.Body);

            // Header'ları kopyala
            foreach (var header in context.Request.Headers)
            {
                if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                {
                    requestMessage.Content?.Headers.TryAddWithoutValidation(
                        header.Key, header.Value.ToArray());
                }
            }

            return await _httpClient.SendAsync(
                requestMessage,
                HttpCompletionOption.ResponseHeadersRead);
        }
    }

}
