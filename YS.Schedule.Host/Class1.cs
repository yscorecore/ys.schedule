using System;
using System.Collections.Generic;
using YS.Schedule;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
namespace YS.Schedule.Host
{
    public abstract class TimerHostedService : IHostedService, IDisposable
    {
        private readonly CancellationTokenSource _stoppingCts =
                                                   new CancellationTokenSource();
        private Timer _timer;

        protected abstract TimeSpan Interval { get; }

        protected virtual TimeSpan WaitTimeBeforeFirstInterval
        {
            get { return TimeSpan.FromSeconds(1); }
        }
        protected virtual bool IntegerSecond
        {
            get { return true; }
        }

        public virtual void Dispose()
        {
            this._timer.Dispose();
            this._stoppingCts.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            TimeSpan waitTime = IntegerSecond ? WaitTimeBeforeFirstInterval + TimeSpan.FromMilliseconds(1000 - DateTimeOffset.Now.Millisecond) : WaitTimeBeforeFirstInterval;
            if (_timer == null)
            {
                _timer = new Timer((s) => Tick((CancellationToken)s), _stoppingCts.Token, waitTime, Interval);
            }
            else
            {
                _timer.Change(waitTime, Interval);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _stoppingCts.Cancel();
            return Task.CompletedTask;
        }

        protected abstract void Tick(CancellationToken state);

    }

    [HostServiceClass]
    public class ScheduleHostedService : TimerHostedService
    {
        public ScheduleHostedService()
        {

        }
        private YS.Lock.ILockService lockService;
        private IEnumerable<IScheduleProvider> providers;
        protected override TimeSpan Interval => TimeSpan.FromSeconds(1);

        protected override void Tick(CancellationToken state)
        {
            var allJobs = providers.AsParallel().SelectMany(p => p.GetAllScheduleJobs().Result);
            var context = new ScheduleContext()
            {
                DateTime = DateTimeOffset.Now,
                Token = state
            };
            Parallel.ForEach(allJobs, p => { RunJob(context, p); });
        }
        protected void RunJob(ScheduleContext context, string job)
        {
            string lockKey = string.Format("{0:yyyy_MM_dd_HH_mm_ss}#{1}", context.DateTime, job);

        }
    }
    public class ScheduleContext
    {
        public DateTimeOffset DateTime { get; set; }
        public CancellationToken Token { get; set; }
    }
}

