using System;
using System.Net.Http;
using InstaSharper.Logger;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Classes.Logger
{
    public class InstaSharperLogger : IInstaLogger
    {
        private readonly ILogger _logger;

        public InstaSharperLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogRequest(HttpRequestMessage request)
        {
            _logger.LogDebug($"Request: {request.Method} {request.RequestUri}");
        }

        public void LogRequest(Uri uri)
        {
            _logger.LogDebug($"Request: {uri}");
        }

        public void LogResponse(HttpResponseMessage response)
        {
            _logger.LogDebug($"Response: {response.RequestMessage.Method} {response.RequestMessage.RequestUri}");
        }

        public void LogException(Exception exception)
        {
            _logger.LogError(exception, "InstaSharper exception");
        }

        public void LogInfo(string info)
        {
            _logger.LogInformation($"Instasharper: {info}");
        }
    }
}