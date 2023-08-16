using System;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ClamarojBack.Utils
{
    public class ErrorLogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorLogger(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogError(Exception ex)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                string logPath = Path.Combine(context.Request.PathBase, "ErrorLog.txt");
                string logMessage = $"[Error Time: {DateTime.Now}] Message: {ex.Message}\nStackTrace: {ex.StackTrace}\n\n";

                File.AppendAllText(logPath, logMessage);
            }
        }
    }
}