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
    public partial class MenuSelectControl : System.Windows.Controls.UserControl
    {
        public MenuSelectControl()
        {
            InitializeComponent();
            MenuList.ItemsSource = App.menuViewModel.Items;
            PaymentListView.ItemsSource = App.tableViewModel.Items[index].Menu;
        }

        private int index;

        public void SetItemIndex(int index)
        {
            Debug.WriteLine(index);
            this.index = index;
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

        private void MenuList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Menu menu = MenuList.SelectedItem as Menu;
            App.tableViewModel.OrderMenu(index, menu);
        }
    }
}
