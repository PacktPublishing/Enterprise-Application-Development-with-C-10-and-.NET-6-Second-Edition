// "//-----------------------------------------------------------------------".
// <copyright file="Policies.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

using Polly;
using Polly.Extensions.Http;

namespace Packt.Ecommerce.Web
{
    /// <summary>
    /// Define the policies used with Polly.
    /// </summary>
    public static class Policies
    {
        /// <summary>
        /// The Retry policy.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        internal static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            Random random = new Random();
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(
                5,
                retry => TimeSpan.FromSeconds(Math.Pow(2, retry))
                                  + TimeSpan.FromMilliseconds(random.Next(0, 100)));
            return retryPolicy;
        }

        /// <summary>
        /// Gets the circuit breaker policy.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        internal static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
