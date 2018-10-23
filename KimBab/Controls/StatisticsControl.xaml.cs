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
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();

        public StatisticsControl()
        {
            InitializeComponent();
            this.DataContext = App.menuViewModel.Items;
            UpdateChart();
        }

        public void UpdateChart()
        {
            MyValue.Clear();
            int i = 0;
            foreach(Model.Menu menu in App.menuViewModel.Items)
            {
                MyValue.Add(new KeyValuePair<string, int>(menu.Name, menu.Orders));
                Debug.WriteLine(MyValue[i++].Value.ToString());
            }

            CategoryChart.DataContext = MyValue;
            CategoryChart.Visibility = Visibility.Visible;
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
