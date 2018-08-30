using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for LoadingControl.xaml
    /// </summary>
    public partial class LoadingControl : UserControl
    {
        const int timerInterval = 1000;

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
            LoadingEndRecieved?.Invoke();
        }
    }
}