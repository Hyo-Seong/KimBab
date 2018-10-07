using KimBab.Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace KimBab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tableListMouseDownIndex;
        private int tableListMouseUpIndex;
        public MainWindow()
        {
            InitializeComponent();
            TableListView.ItemsSource = App.tableViewModel.Items;
            TableListView.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(TableListView_MouseLeftButtonDown), true);
        }

        private void MenuSelectControl_OnPaymentControlStatusRecieved(object sender, int tableNum)
        {
            PaymentControl.SetTable(tableNum);
            PaymentControl.Visibility = Visibility.Visible;
        }

        //private void SetLoginProgressRing(bool isActive)
        //{
        //    progressRing.IsActive = isActive;
        //    recLogin.Visibility = isActive ? Visibility.Visible : Visibility.Collapsed;
        //} 참고 (usercontrol 제어, 메신져코드)

        private void LoadingControl_LoadingEndRecieved()
        {
            LoadingControl.Visibility = Visibility.Collapsed;
        }

        private void MenuSelectControl_HideControl()
        {
            MenuSelectControl.Visibility = Visibility.Collapsed;
            TableListView.ItemsSource = null;
            TableListView.ItemsSource = App.tableViewModel.Items;
        }

        private void TableListView_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tableListMouseDownIndex = TableListView.SelectedIndex;
        }

        private void TableListView_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tableListMouseUpIndex = TableListView.SelectedIndex;
            if (tableListMouseDownIndex != tableListMouseUpIndex)
            {
                Debug.WriteLine("Canceled");
                return;
            }
            if (tableListMouseUpIndex == -1)
            {
                return;
            }

            Table selectTable;
            try
            {
                selectTable = TableListView.SelectedItem as Table;
                MenuSelectControl.SetItemIndex(selectTable.TableNum - 1);
                MenuSelectControl.Visibility = Visibility.Visible;
                //TableListView.SelectedIndex = -1;
            }
            catch
            {
                return;
            }
        }

        private void WindowControlBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Visibility visibilty = Visibility.Visible;
            switch (button.Content)
            {
                case "좌석":
                    visibilty = Visibility.Collapsed;
                    break;
                case "통계":
                    visibilty = Visibility.Visible;
                    break;
            }
            StatisticsControl.Visibility = visibilty;
        }
    }
}