using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        private List<KeyValuePair<string, int>> CategoryChartList = new List<KeyValuePair<string, int>>();
        private List<KeyValuePair<string, int>> MenuChartList = new List<KeyValuePair<string, int>>();

        public StatisticsControl()
        {
            InitializeComponent();
            this.DataContext = App.menuViewModel.Items;
            UpdateChart();
        }

        public void UpdateChart()
        {
            InitList();

            int i = 0;
            int[] typeCountArray = new int[] { 0, 0, 0, 0, 0 };
            foreach (Model.Menu menu in App.menuViewModel.Items)
            {
                MenuChartList.Add(new KeyValuePair<string, int>(menu.Name, menu.Orders));
                switch (menu.Type.ToString())
                {
                    case "KIMBAB":
                        typeCountArray[0] += menu.Orders;
                        break;

                    case "NOODLE":
                        typeCountArray[1] += menu.Orders;
                        break;

                    case "SIKSA":
                        typeCountArray[2] += menu.Orders;
                        break;

                    case "BUNSIK":
                        typeCountArray[3] += menu.Orders;
                        break;

                    case "DONGAS":
                        typeCountArray[4] += menu.Orders;
                        break;
                }
            }
            SetCategoryList(typeCountArray);

            CategoryChart.DataContext = null;
            CategoryChart.DataContext = CategoryChartList;
            MenuChart.DataContext = null;
            MenuChart.DataContext = MenuChartList;
        }

        private void SetCategoryList(int[] array)
        {
            CategoryChartList.Add(new KeyValuePair<string, int>("KIMBAB", array[0]));
            CategoryChartList.Add(new KeyValuePair<string, int>("NOODLE", array[1]));
            CategoryChartList.Add(new KeyValuePair<string, int>("SIKSA", array[2]));
            CategoryChartList.Add(new KeyValuePair<string, int>("BUNSIK", array[3]));
            CategoryChartList.Add(new KeyValuePair<string, int>("DONGAS", array[4]));
        }

        private void InitList()
        {
            CategoryChartList.Clear();
            MenuChartList.Clear();
        }

        private void ChangeChartBtn_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            if (button.Content.Equals("카테고리별 판매량"))
            {
                button.Content = "메뉴별 판매량";
                CategoryChart.Visibility = Visibility.Hidden;
                MenuChart.Visibility = Visibility.Visible;
            }
            else
            {
                button.Content = "카테고리별 판매량";
                CategoryChart.Visibility = Visibility.Visible;
                MenuChart.Visibility = Visibility.Hidden;
            }
        }
    }
}