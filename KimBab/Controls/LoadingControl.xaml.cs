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
        private const int timerInterval = 30;

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
            // LoadingEndRecieved?.Invoke();
            if (LoadingProgressBar.Value >= LoadingProgressBar.Maximum)
            {
                LoadingEndRecieved?.Invoke();
            }
        }
    }
}