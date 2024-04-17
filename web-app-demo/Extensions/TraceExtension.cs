using System;
using System.Text.Json;
using web_app_demo;

public static class TraceExtension
{
    public static string TraceFormat(string trace)
    {
        if (trace != null)
        {
            var traceCollection = trace.Split("DEBUG:");

            var obj = new { data = traceCollection.Where(t => t is not "").Select(t => new { message = t }) };
            return JsonSerializer.Serialize(obj);
        }
        return string.Empty;
    }
}