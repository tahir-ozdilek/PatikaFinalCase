using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PatikaFinalProject.Common
{
    public class ExceptionLoggerMiddleware
    {
        RequestDelegate requestDelegate;

        public ExceptionLoggerMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            string logMessage = context.Request?.Method + context.Request?.Path.Value + context.Response?.StatusCode;
            try
            {
                Console.WriteLine(logMessage);
                await requestDelegate(context);
            }
            catch(Exception ex)
            {
                Console.WriteLine(logMessage + " " + ex.Message);

                context.Response?.WriteAsJsonAsync(JsonConvert.SerializeObject(ex.Message));
            }
        }
    }
}
