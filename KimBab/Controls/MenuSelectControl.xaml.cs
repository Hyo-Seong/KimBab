using KimBab.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Menu = KimBab.Model.Menu;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for MenuSelectControl.xaml
    /// </summary>
    ///

    public partial class MenuSelectControl : System.Windows.Controls.UserControl
    {
        public delegate void onPaymentControlStatusRecievedHandler(object sender, int tableNum);

        public event onPaymentControlStatusRecievedHandler OnPaymentControlStatusRecieved;

        private int menuListMouseDownIndex;
        private int menuListMouseUpIndex;

        private string barcode;

        public delegate void hideControlHandler();

        public event hideControlHandler OnHideControlRecieved;
        

        public MenuSelectControl()
        {
            InitializeComponent();
            MenuList.ItemsSource = App.menuViewModel.Items;
            MenuList.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MenuList_MouseLeftButtonDown), true);
        }

        private int tableNum;

        public void SetItemIndex(int index)
        {
            this.tableNum = index;

            App.tableViewModel.SetTempItems(tableNum);

            PaymentListView.ItemsSource = null;
            PaymentListView.ItemsSource = App.tableViewModel.Items[tableNum].MenuList;
            this.DataContext = App.tableViewModel.Items[tableNum];
            TableNumLabel.Content = (tableNum + 1) + "번 테이블";
            SelectedMenuImage.Source = null;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            List<Menu> menus = new List<Menu>();
            FoodType foodType;
            var element = (FrameworkElement)sender;
            switch (element.Name)
            {
                case "KimbabBtn":
                    foodType = FoodType.KIMBAB;
                    break;

                case "NoodleBtn":
                    foodType = FoodType.NOODLE;
                    break;

                case "SiksaBtn":
                    foodType = FoodType.SIKSA;
                    break;

                case "BunsikBtn":
                    foodType = FoodType.BUNSIK;
                    break;

                case "DongasBtn":
                    foodType = FoodType.DONGAS;
                    break;
                case "DrinkBtn":
                    foodType = FoodType.DRINK;
                    break;
                case "AllBtn":
                    MenuList.ItemsSource = App.menuViewModel.Items;
                    return;

                default:
                    Debug.WriteLine("ERROR!");
                    return;
            }
            foreach (var c in App.menuViewModel.Items)
            {
                if (c.Type == foodType)
                {
                    menus.Add(c);
                }
            }
            MenuList.ItemsSource = menus;
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            App.tableViewModel.CancelOrder(tableNum);
            OnHideControlRecieved?.Invoke();
        }

        private void MenuList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            menuListMouseUpIndex = MenuList.SelectedIndex;
            if (menuListMouseUpIndex != menuListMouseDownIndex)
            {
                Debug.WriteLine("Canceled");
                return;
            }
            if (menuListMouseUpIndex == -1)
            {
                return;
            }

            
            try
            {
                Menu menu = MenuList.SelectedItem as Menu;
                SelectedMenuImage.Source = SetImageSource(menu.Image);

                App.tableViewModel.AddOrderMenu(tableNum, menu);
                PaymentListView.ItemsSource = null;
                PaymentListView.ItemsSource = App.tableViewModel.Items[tableNum].MenuList;
                MenuList.SelectedIndex = -1;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("MenuSelect Exception : " + exception.Message);
                return;
            }
        }

        private void MenuList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuListMouseDownIndex = MenuList.SelectedIndex;
        }

        private void PaymentControlBtn_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = PaymentListView.SelectedIndex;
            if (selectedIndex == -1)
            {
                Debug.WriteLine("선택되지않음.");
                return;
            }
            Button clickBtn = sender as Button;

            var temp = PaymentListView.SelectedItem as Menu;
            switch (clickBtn.Content)
            {
                case "취소":
                    App.tableViewModel.CancelMenu(tableNum, selectedIndex);
                    break;

                case "+":
                    App.tableViewModel.PlusOrder(tableNum, selectedIndex);
                    break;

                case "-":
                    App.tableViewModel.MinusOrder(tableNum, selectedIndex);

                    break;
            }
            try
            {
                PaymentListView.ItemsSource = null;
                PaymentListView.ItemsSource = App.tableViewModel.Items[tableNum].MenuList;
                if (App.tableViewModel.Items[tableNum].MenuList[selectedIndex].Name == temp.Name) //
                {
                    PaymentListView.SelectedIndex = selectedIndex;   
                }
            } catch(Exception exception)
            {
                Debug.WriteLine("PaymentListView Error : " + exception.Message);
            }


        }

        private void PaymentListDelete_Click(object sender, RoutedEventArgs e)
        {
            App.tableViewModel.ClearList(tableNum);
            PaymentListView.ItemsSource = null;
            PaymentListView.ItemsSource = App.tableViewModel.Items[tableNum].MenuList;
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.tableViewModel.Items[tableNum].MenuList.Count == 0)
            {
                return;
            }
            //결제를 하기전에 주문은 필수니까.
            Order(Visibility.Visible);
            OnPaymentControlStatusRecieved?.Invoke(null, tableNum);
        }

        private BitmapImage SetImageSource(string imagePath)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(imagePath);
            logo.EndInit();
            return logo;
        }

        private void PaymentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Menu menu = PaymentListView.SelectedItem as Menu;
                SelectedMenuImage.Source = SetImageSource(menu.Image);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            Order(Visibility.Collapsed);
        }

        private void Order(Visibility visibility)
        {
            App.tableViewModel.SetMenuString(tableNum);
            if (App.tableViewModel.CheckIsChanged(tableNum))
            {
                return;
            }
            bool isNull;
            if (App.tableViewModel.Items[tableNum].MenuList.Count == 0)
            {
                isNull = true;
            }
            else
            {
                isNull = false;
            }
            App.tableViewModel.SetOrderDateTime(tableNum, isNull);
            this.Visibility = visibility;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
		{
            if(this.Visibility != Visibility.Visible)
            {
                return;
            }
            try
            {
                if (e.Key == Key.Return)
                {
                    CheckBarcode(barcode.Substring(barcode.Length - 13));
                    barcode = "";
                }
                barcode += e.Key.ToString().Replace("D", string.Empty);
            } catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                barcode = "";
            }
        }

        private void CheckBarcode(string barcodeStr)
        {
            for(int i=0;i<MenuList.Items.Count; i++)
            {
                Menu menu = MenuList.Items[i] as Menu;
                if (menu.Barcode.ToString() == barcodeStr)
                {
                    MenuList.SelectedIndex = i;
                    menuListMouseDownIndex = i;
                    MenuList_MouseLeftButtonUp(null, null);
                }
            }
        }

        // https://stackoverflow.com/questions/347724/how-can-i-capture-keydown-event-on-a-wpf-page-or-usercontrol-object
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }
    }
}