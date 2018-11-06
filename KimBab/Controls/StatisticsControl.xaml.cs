using KimBab.Model;
using System;
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
        private List<KeyValuePair<string, int>> CategoryOrdersChartList = new List<KeyValuePair<string, int>>();
        private List<KeyValuePair<string, int>> CategoryOrderPriceChartList = new List<KeyValuePair<string, int>>();
        private List<KeyValuePair<string, int>> MenuOrdersChartList = new List<KeyValuePair<string, int>>();
        private List<KeyValuePair<string, int>> MenuOrderPriceChartList = new List<KeyValuePair<string, int>>();

        List<CategoryCount> typeCountList = new List<CategoryCount>();

        private const int FOODTYPE_COUNT = 6;
        public StatisticsControl()
        {
            InitializeComponent();
            this.DataContext = App.menuViewModel.Items;
            UpdateChart();
        }



        public void UpdateChart()
        {
            InitList();

            
            // 메뉴별 판매량
            // 메뉴별 총액
            // 카테고리별 판매량
            // 카테고리별 총액

            foreach (Model.Menu menu in App.menuViewModel.Items)
            {
                MenuOrdersChartList.Add(new KeyValuePair<string, int>(menu.Name, menu.Orders)); // 메뉴별 판매량 통계
                MenuOrderPriceChartList.Add(new KeyValuePair<string, int>(menu.Name, menu.OrderPrice));
                int i = 0;
                switch (menu.Type.ToString())
                {
                    case "KIMBAB":
                        i = 0;
                        break;

                    case "NOODLE":
                        i = 1;
                        break;

                    case "SIKSA":
                        i = 2;
                        break;

                    case "BUNSIK":
                        i = 3;
                        break;

                    case "DONGAS":
                        i = 4;
                        break;
                    case "DRINK":
                        i = 5;
                        break;
                }
                typeCountList[i].Orders += menu.Orders;
                typeCountList[i].OrderPrice += menu.OrderPrice;
            }
            SetCategoryList(typeCountList);


            SetChartDataContext();
        }

        #region DataContext설정
        private void SetChartDataContext()
        {
            CategoryOrderPriceChart.DataContext = null;
            CategoryOrdersChart.DataContext = null;
            MenuOrderPriceChart.DataContext = null;
            MenuOrdersChart.DataContext = null;

            CategoryOrderPriceChart.DataContext = CategoryOrderPriceChartList;
            CategoryOrdersChart.DataContext = CategoryOrdersChartList;
            MenuOrderPriceChart.DataContext = MenuOrderPriceChartList;
            MenuOrdersChart.DataContext = MenuOrdersChartList;
        }
        #endregion

        private void InitTypeCountList()
        {
            for(int i = 0; i<FOODTYPE_COUNT; i++)
            {
                typeCountList.Add(new CategoryCount { Orders = 0, OrderPrice = 0 });
            }
        }

        private void SetCategoryList(List<CategoryCount> array)
        {
            for(int i = 0; i<FOODTYPE_COUNT; i++)
            {
                CategoryOrderPriceChartList.Add(new KeyValuePair<string, int>("" + (Model.FoodType)i, array[i].OrderPrice));
                CategoryOrdersChartList.Add(new KeyValuePair<string, int>("" + (Model.FoodType)i, array[i].Orders));
            }
        }

        private void InitList()
        {
            CategoryOrdersChartList.Clear();
            CategoryOrderPriceChartList.Clear();
            MenuOrdersChartList.Clear();
            MenuOrderPriceChartList.Clear();
            typeCountList.Clear();

            InitTypeCountList();
        }

        private void ChangeChartBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.Equals("카테고리별 판매량"))
            {
                button.Content = "메뉴별 판매량";
                CategoryChartGd.Visibility = Visibility.Collapsed;
                MenuChartGd.Visibility = Visibility.Visible;
            }
            else
            {
                button.Content = "카테고리별 판매량";
                CategoryChartGd.Visibility = Visibility.Visible;
                MenuChartGd.Visibility = Visibility.Collapsed;
            };
        }
    }

    public class CategoryCount{
        public int Orders;
        public int OrderPrice;
        }
}