#define DEBUG

using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for LoadingControl.xaml
    /// </summary>
    public partial class LoadingControl : UserControl
    {
        private const int timerInterval = 1; //TODO: 제출전에 30으로 수정해야됨.

        public delegate void loadingEndRecievedHandler();

        public event loadingEndRecievedHandler LoadingEndRecieved;

        public LoadingControl()
        {
            InitializeComponent();
            InitTimer();
        }

        private void InitTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            LoadingProgressBar.Value++;
#if DEBUG
            LoadingEndRecieved?.Invoke();
#else
            if (LoadingProgressBar.Value >= LoadingProgressBar.Maximum)
            {
                LoadingEndRecieved?.Invoke();
            }
#endif
        }
    }
}