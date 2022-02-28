using System;
using System.Threading;

namespace GraphX
{
    public class BManualResetEvent : BWaitHandle, IDisposable
    {
        ManualResetEvent _mre;

        public BManualResetEvent(bool initialState)
        {
            _mre = new ManualResetEvent(initialState);
        }

        // Summary:
        //     Sets the state of the event to non-signaled, which causes threads to block.
        //
        // Returns:
        //     true if the operation succeeds; otherwise, false.
        public bool Reset() => _mre.Reset();

        //
        // Summary:
        //     Sets the state of the event to signaled, which allows one or more waiting
        //     threads to proceed.
        //
        // Returns:
        //     true if the operation succeeds; otherwise, false.
        public bool Set()
        {
            return _mre.Set();
        }

        protected override void OnSuccessfullWait()
        {
            // nothing special needed
        }

        public override bool WaitOne() => _mre.WaitOne();

        public override bool WaitOne(TimeSpan timeout) => _mre.WaitOne(timeout);

        public override bool WaitOne(int millisecondsTimeout) => _mre.WaitOne(millisecondsTimeout);

        internal override WaitHandle WaitHandle => _mre;

        public void Dispose()
        {
            if (_mre == null) return;
            _mre.Dispose();
            _mre = null;
        }
    }
}
