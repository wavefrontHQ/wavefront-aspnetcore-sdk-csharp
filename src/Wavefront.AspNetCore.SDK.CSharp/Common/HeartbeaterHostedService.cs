﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Wavefront.SDK.CSharp.Common.Application;

namespace Wavefront.AspNetCore.SDK.CSharp.Common
{
    /// <summary>
    ///     Hosted service (activated at application startup) that runs a
    ///     <see cref="HeartbeaterService"/> as a background task.
    /// </summary>
    public class HeartbeaterHostedService : IHostedService, IDisposable
    {
        private readonly HeartbeaterService heartbeaterService;

        public HeartbeaterHostedService(WavefrontAspNetCoreReporter wfAspNetCoreReporter)
        {
            heartbeaterService = new HeartbeaterService(
                wfAspNetCoreReporter.WavefrontSender,
                wfAspNetCoreReporter.ApplicationTags,
                Constants.AspNetCoreComponent,
                wfAspNetCoreReporter.Source
            );
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            heartbeaterService.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            heartbeaterService.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            heartbeaterService.Dispose();
        }
    }
}
