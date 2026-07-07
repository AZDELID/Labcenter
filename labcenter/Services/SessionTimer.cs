using System;
using System.Windows.Forms;

namespace labcenter.Services
{
    public class SessionTimer : IDisposable
    {
        private readonly Timer timer;
        private int elapsedSeconds;

        public SessionTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        public event EventHandler TimeChanged;

        public string TimeText
        {
            get
            {
                int h = elapsedSeconds / 3600;
                int m = (elapsedSeconds % 3600) / 60;
                int s = elapsedSeconds % 60;

                return string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Dispose()
        {
            timer.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            OnTimeChanged();
        }

        private void OnTimeChanged()
        {
            if (TimeChanged != null)
                TimeChanged(this, EventArgs.Empty);
        }
    }
}
