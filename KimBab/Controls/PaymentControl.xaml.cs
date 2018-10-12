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
        public delegate void ChangeVisible(Visibility visibility);
        public event ChangeVisible onChangeVisible;

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
                App.menuViewModel.AddStatistics(tableNum);
                // todo : 여기서 메인화면으로 넘기기. (이벤트로 넘겨야할듯..?)
                onChangeVisible?.Invoke(Visibility.Collapsed);
            } else
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
