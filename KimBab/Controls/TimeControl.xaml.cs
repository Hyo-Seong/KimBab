using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for TimeControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl
    {
        public TimeControl()
        {
            InitializeComponent();

            InitTimer();
        }

        private void InitTimer()
        {
            TimeTextBlock.Text = DateTime.Now.ToString();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeTextBlock.Text = DateTime.Now.ToString();
        }
    }
}