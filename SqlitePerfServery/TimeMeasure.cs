using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePerfServery
{
    internal class TimeMeasure
    {
        private Stopwatch _StopWatch = new Stopwatch();

        public void Start()
        {
            _StopWatch.Start();
        }

        public void Stop()
        {
            _StopWatch.Stop();
        }

        public string Elapsed()
        {
            var ts = _StopWatch.Elapsed;
            return $"{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
        }
    }
}
