using System;
using System.Diagnostics;

namespace DataSource
{
    public sealed class TimeMeasurer : IDisposable
    {
        private readonly Action<TimeSpan> _outputAction;
        private readonly Stopwatch _stopWatch;

        public TimeMeasurer(Action<TimeSpan> outputAction)
        {
            _outputAction = outputAction ?? throw new ArgumentNullException(nameof(outputAction));
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        void IDisposable.Dispose()
        {
            _stopWatch.Stop();
            _outputAction(_stopWatch.Elapsed);
        }
    }
}
