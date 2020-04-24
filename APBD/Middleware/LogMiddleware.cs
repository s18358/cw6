using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace APBD.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                string[] data = { httpContext.Request.Path, httpContext.Request.Method, httpContext.Request?.QueryString.ToString(), "" };

                using (StreamReader reader
                    = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    data[3] = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }

                var writer = new FileStream("requestLog.txt", FileMode.Append);

                using (var streamWriter = new StreamWriter(writer))
                {
                    string output = $"Path: {data[0]} \nQueryString:{data[1]} \nMethod: {data[2]} \nBody Parameters: {data[3]}";
                    streamWriter.WriteLine(output);
                }
            }

            await _next(httpContext);
        }
    }
}
