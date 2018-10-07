using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for PaymentControl.xaml
    /// </summary>
    public partial class PaymentControl : UserControl
    {
        public PaymentControl()
        {
            InitializeComponent();
        }

        private int tableNum = -1;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Button button = sender as Button;
            if (button.Content.Equals("결제"))
            {
                Debug.WriteLine("aa");
                App.menuViewModel.AddStatistics(tableNum);
            }
        }

        public void SetTable(int tableNum)
        {
            this.tableNum = tableNum;
            this.DataContext = App.tableViewModel.Items[tableNum];
        }
    }
}
