using System.Diagnostics;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PersonalFinancialManagement.Common.Interceptors;

public class MonitoringInterceptor : AsyncTimingInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger _logger;

    public MonitoringInterceptor(ILogger<MonitoringInterceptor> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void CompletedTiming(IInvocation invocation, Stopwatch stopwatch)
    {
        var requestCtx = InitRequest();
        _logger.LogInformation(
            $"[PERF] - RequestId - [{requestCtx.Item1}] - Method {ToStringInvocation(invocation)} completed in {stopwatch.ElapsedMilliseconds}ms");
    }

    protected override void StartingTiming(IInvocation invocation)
    {
        var requestCtx = InitRequest();
        _logger.LogInformation(
            $"[PERF] - RequestId - [{requestCtx.Item1}] - Method {ToStringInvocation(invocation)} invoked!");
    }

    private Tuple<string?, DateTime> InitRequest()
    {
        var startTime = DateTime.UtcNow;
        var request = _httpContextAccessor.HttpContext;
        try
        {
            if (request.Items.TryGetValue("_RequestStartedAt", out var item))
                startTime = (DateTime)item;
            else
                request.Items["_RequestStartedAt"] = startTime;
        }
        catch
        {
            // ignored
        }

        return Tuple.Create(request?.TraceIdentifier, startTime);
    }

    private string ToStringInvocation(IInvocation invocation)
    {
        return invocation.MethodInvocationTarget != null
            ? $"{invocation.MethodInvocationTarget.ReflectedType?.FullName}.{invocation.MethodInvocationTarget.Name}"
            : string.Empty;
    }
}