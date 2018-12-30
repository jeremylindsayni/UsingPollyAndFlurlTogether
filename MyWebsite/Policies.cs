using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace MyWebsite
{
    public static class Policies
    {
        private static TimeoutPolicy<HttpResponseMessage> TimeoutPolicy
        {
            get
            {
                return Policy.TimeoutAsync<HttpResponseMessage>(2, (context, timeSpan, task) =>
                {
                    Debug.WriteLine("[App|Policy]: Timeout delegate fired after " + timeSpan.TotalMilliseconds);
                    return Task.CompletedTask;
                });
            }
        }

        private static RetryPolicy<HttpResponseMessage> RetryPolicy
        {
            get
            {
                return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .Or<TimeoutRejectedException>()
                    .RetryAsync(3, (delegateResult, i) =>
                    {
                        Debug.WriteLine("[App|Policy]: Retry delegate fired, attempt " + i);
                        return Task.CompletedTask;
                    });
            }
        }

        public static PolicyWrap<HttpResponseMessage> PolicyStrategy => Policy.WrapAsync(RetryPolicy, TimeoutPolicy);
    }
}