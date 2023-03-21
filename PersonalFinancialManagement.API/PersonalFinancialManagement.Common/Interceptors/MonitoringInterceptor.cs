using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PersonalFinancialManagement.Common.Interceptors
{
    public class MonitoringInterceptor : AsyncTimingInterceptor
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MonitoringInterceptor(ILogger<MonitoringInterceptor> logger, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void CompletedTiming(IInvocation invocation, Stopwatch stopwatch)
        {
            var requestCtx = InitRequest();
            _logger.LogInformation($"[PERF] - RequestId - [{requestCtx.Item1}] - Method {ToStringInvocation(invocation)} completed in {stopwatch.ElapsedMilliseconds}ms");
        }

        protected override void StartingTiming(IInvocation invocation)
        {
            var requestCtx = InitRequest();
            _logger.LogInformation($"[PERF] - RequestId - [{requestCtx.Item1}] - Method {ToStringInvocation(invocation)} invoked!");
        }

        private Tuple<string, DateTime> InitRequest()
        {
            var startTime = DateTime.UtcNow;
            var request = _httpContextAccessor.HttpContext;
            if (request.Items.ContainsKey("_RequestStartedAt"))
            {
                startTime = (DateTime)request.Items["_RequestStartedAt"];
            }
            else
            {
                request.Items["_RequestStartedAt"] = startTime;
            }
            return Tuple.Create(request.TraceIdentifier, startTime);
        }

        private string ToStringInvocation(IInvocation invocation)
        {
            var result = string.Empty;
            if (invocation.MethodInvocationTarget != null)
            {
                return $"{invocation.MethodInvocationTarget.ReflectedType?.FullName}.{invocation.MethodInvocationTarget.Name}";
            }
            return result.ToString();
        }
    }
}
