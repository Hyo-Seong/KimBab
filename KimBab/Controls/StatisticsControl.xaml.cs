using KimBab.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Menu = KimBab.Model.Menu;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        private List<KeyValuePair<string, int>> CategoryOrdersChartList = new List<KeyValuePair<string, int>>();    //  카테고리별 판매량
        private List<KeyValuePair<string, int>> CategoryOrderPriceChartList = new List<KeyValuePair<string, int>>();//  카테고리별 판매액
        private List<KeyValuePair<string, int>> MenuOrdersChartList = new List<KeyValuePair<string, int>>();        //  메뉴별 판매량
        private List<KeyValuePair<string, int>> MenuOrderPriceChartList = new List<KeyValuePair<string, int>>();    //  메뉴별 판매액
        private List<KeyValuePair<string, int>> PaymentOrdersChartList = new List<KeyValuePair<string, int>>();     //  결제방식별 판매액
        private List<KeyValuePair<string, int>> PaymentOrderPriceChartList = new List<KeyValuePair<string, int>>(); //  결제방식별 판매략

        List<CategoryCount> menuCountList = new List<CategoryCount>();    //  메뉴별 통계 temp list
        List<CategoryCount> paymentCountList = new List<CategoryCount>(); //  결제방식별 통계 temp list
        List<CategoryCount> typeCountList = new List<CategoryCount>();    //  카테고리별 통계 temp list

        private const int MENU_COUNT = 32;
        private const int FOODTYPE_COUNT = 6;
        private const int PAYMENTTYPE_COUNT = 2;

        public StatisticsControl()
        {
            InitializeComponent();
            this.DataContext = App.menuViewModel.Items;
            UpdateChart();
        }



        public void UpdateChart()
        {
            InitList();

            for(int i=0; i<App.tableViewModel.statistics.Count; i++){
                foreach(Menu menu in App.tableViewModel.statistics[i].MenuList)
                {
                    int pType = 0;
                    int categoryIdx = 0;

                    menuCountList[menu.Idx].Orders += menu.Orders;
                    menuCountList[menu.Idx].OrderPrice += menu.OrderPrice;

                    switch (menu.Type.ToString()) // 카테고리별 통계
                    {
                        case "KIMBAB":
                            categoryIdx = 0;
                            break;

                        case "NOODLE":
                            categoryIdx = 1;
                            break;

                        case "SIKSA":
                            categoryIdx = 2;
                            break;

                        case "BUNSIK":
                            categoryIdx = 3;
                            break;

                        case "DONGAS":
                            categoryIdx = 4;
                            break;
                        case "DRINK":
                            categoryIdx = 5;
                            break;
                    }
                    typeCountList[categoryIdx].Orders += menu.Orders;
                    typeCountList[categoryIdx].OrderPrice += menu.OrderPrice;

                    if (App.tableViewModel.statistics[i].Payment == PaymentType.CASH)
                    {
                        pType = 0; // CASH
                    }
                    else
                    {
                        pType = 1; // CARD
                    }
                    paymentCountList[pType].Orders += menu.Orders;
                    paymentCountList[pType].OrderPrice += menu.OrderPrice;
                }
            }
            SetList();

            SetChartDataContext();

        }

        private void SetList()
        {
            SetMenuList(menuCountList);
            SetPaymentList(paymentCountList);
            SetCategoryList(typeCountList);
        }

        private void SetMenuList(List<CategoryCount> menuCountList)
        {
            for (int i = 0; i < MENU_COUNT; i++)
            {
                MenuOrdersChartList.Add(new KeyValuePair<string, int>(App.menuViewModel.Items[i].Name, menuCountList[i].Orders));
                MenuOrderPriceChartList.Add(new KeyValuePair<string, int>(App.menuViewModel.Items[i].Name, menuCountList[i].OrderPrice));
            }
        }

        private void SetPaymentList(List<CategoryCount> paymentCountList)
        {
            for(int i=0; i<PAYMENTTYPE_COUNT; i++)
            {
                PaymentOrdersChartList.Add(new KeyValuePair<string, int>("" + (PaymentType)i, paymentCountList[i].Orders));
                PaymentOrderPriceChartList.Add(new KeyValuePair<string, int>("" + (PaymentType)i, paymentCountList[i].OrderPrice));
            }
        }

        private void SetCategoryList(List<CategoryCount> typeCountList)
        {
            for (int i = 0; i < FOODTYPE_COUNT; i++)
            {
                CategoryOrderPriceChartList.Add(new KeyValuePair<string, int>("" + (FoodType)i, typeCountList[i].OrderPrice));
                CategoryOrdersChartList.Add(new KeyValuePair<string, int>("" + (FoodType)i, typeCountList[i].Orders));
            }
        }

        #region DataContext설정
        private void SetChartDataContext()
        {
            CategoryOrderPriceChart.DataContext = null;
            CategoryOrdersChart.DataContext = null;
            MenuOrderPriceChart.DataContext = null;
            MenuOrdersChart.DataContext = null;
            PaymentOrderPriceChart.DataContext = null;
            PaymentOrdersChart.DataContext = null;

            CategoryOrderPriceChart.DataContext = CategoryOrderPriceChartList;
            CategoryOrdersChart.DataContext = CategoryOrdersChartList;
            MenuOrderPriceChart.DataContext = MenuOrderPriceChartList;
            MenuOrdersChart.DataContext = MenuOrdersChartList;
            PaymentOrderPriceChart.DataContext = PaymentOrderPriceChartList;
            PaymentOrdersChart.DataContext = PaymentOrdersChartList;
        }
        #endregion

        private void InitList()
        {
            CategoryOrdersChartList.Clear();
            CategoryOrderPriceChartList.Clear();
            MenuOrdersChartList.Clear();
            MenuOrderPriceChartList.Clear();
            PaymentOrdersChartList.Clear();
            PaymentOrderPriceChartList.Clear();

            menuCountList.Clear();
            typeCountList.Clear();
            paymentCountList.Clear();

            InitCountList();
        }

        private void InitCountList()
        {
            for(int i = 0; i < MENU_COUNT; i++)
            {
                menuCountList.Add(new CategoryCount { OrderPrice = 0, Orders = 0 });
            }

            for (int i = 0; i < PAYMENTTYPE_COUNT; i++) // Init PaymentCountList
            {
                paymentCountList.Add(new CategoryCount { OrderPrice = 0, Orders = 0 });
            }

            for (int i = 0; i < FOODTYPE_COUNT; i++) // Init TypeCountList
            {
                typeCountList.Add(new CategoryCount { Orders = 0, OrderPrice = 0 });
            }
        }

        private void ChangeChartBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SetCollapsed();
            switch (button.Content)
            {
                case "카테고리별 통계":
                    CategoryChartGd.Visibility = Visibility.Visible;
                    break;
                case "메뉴별 통계":
                    MenuChartGd.Visibility = Visibility.Visible;
                    break;
                case "결제방식별 통계":
                    PaymentChartGd.Visibility = Visibility.Visible;
                    break;
                default:
                    CategoryChartGd.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SetCollapsed()
        {
            CategoryChartGd.Visibility = Visibility.Collapsed;
            MenuChartGd.Visibility = Visibility.Collapsed;
            PaymentChartGd.Visibility = Visibility.Collapsed;
        }
    }

    public class CategoryCount{
        public int Orders;
        public int OrderPrice;
        }
}