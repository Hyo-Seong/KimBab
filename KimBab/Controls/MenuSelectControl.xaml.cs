using KimBab.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KimBab.Controls
{
    /// <summary>
    /// Interaction logic for MenuSelectControl.xaml
    /// </summary>
    /// 



    public partial class MenuSelectControl : System.Windows.Controls.UserControl
    {
        private int menuListMouseDownIndex;
        private int menuListMouseUpIndex;
        public delegate void hideControlHandler();
        public event hideControlHandler HideControl;
        public MenuSelectControl()
        {
            InitializeComponent();
            MenuList.ItemsSource = App.menuViewModel.Items;
            MenuList.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MenuList_MouseLeftButtonDown), true);
        }

        private int tableNum;

        public void SetItemIndex(int index)
        {
            Debug.WriteLine("index : " + index);
            this.tableNum = index;
            PaymentListView.ItemsSource = App.tableViewModel.Items[tableNum].Menu;
            this.DataContext = App.tableViewModel.Items[tableNum];
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
            //TODO : 여기서 MenuString 정의해야됨!
            PaymentListView.ItemsSource = null;
            App.tableViewModel.SetMenuString(tableNum);
            HideControl?.Invoke();
        }

        private void MenuList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            menuListMouseUpIndex = MenuList.SelectedIndex;
            if(menuListMouseUpIndex != menuListMouseDownIndex)
            {
                Debug.WriteLine("Canceled");
                return;
            }
            if (menuListMouseUpIndex == -1)
            {
                return;
            }
            
            Menu menu;
            try
            {
                menu = MenuList.SelectedItem as Menu;
                App.tableViewModel.AddOrderMenu(tableNum, menu);
                PaymentListView.ItemsSource = null;
                PaymentListView.ItemsSource = App.tableViewModel.Items[tableNum].Menu;
                MenuList.SelectedIndex = -1;
            }
            catch
            {
                return;
            }
        }

        private void MenuList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuListMouseDownIndex = MenuList.SelectedIndex;
        }
    }
}