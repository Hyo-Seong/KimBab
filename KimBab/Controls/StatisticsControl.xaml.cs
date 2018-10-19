using System;
using System.Collections.Generic;
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
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        public StatisticsControl()
        {
            InitializeComponent();
            this.DataContext = App.menuViewModel.Items[1];
        }

        private void ChangeChartBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(button.Content.Equals("카테고리별 판매량"))
            {
                button.Content = "메뉴별 판매량";
            }
            else
            {
                button.Content = "카테고리별 판매량";
            }
        }
    }
}
