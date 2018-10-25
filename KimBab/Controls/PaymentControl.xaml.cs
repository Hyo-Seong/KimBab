using System.Windows;
using System.Windows.Controls;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for PaymentControl.xaml
    /// </summary>
    public partial class PaymentControl : UserControl
    {
        public delegate void ChangeVisible(Visibility visibility);

        public event ChangeVisible onChangeVisible;

        public PaymentControl()
        {
            InitializeComponent();
        }

        private int tableNum = -1;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Button button = sender as Button;
            if (button.Content.Equals("결제"))
            {
                App.menuViewModel.AddStatistics(tableNum);
                onChangeVisible?.Invoke(Visibility.Collapsed);
            }
            else
            {
                return;
            }
        }

        public void SetTable(int tableNum)
        {
            this.tableNum = tableNum;
            this.DataContext = App.tableViewModel.Items[tableNum];
        }
    }
}