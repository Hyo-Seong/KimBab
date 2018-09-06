using KimBab.Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace KimBab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataListView.ItemsSource = App.tableViewModel.Items;
        }

        private void DataListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            Table data = (Table)DataListView.SelectedItem;
            //DataListView.SelectedItem = null;
            if (data != null)
            {
                MenuSelectControl.SetItemIndex(data.TableNum - 1);
                MenuSelectControl.Visibility = Visibility.Visible;

                Debug.WriteLine(data.TableNum);
            }
        }

        private void LoadingControl_LoadingEndRecieved()
        {
            LoadingControl.Visibility = Visibility.Collapsed;
        }

        private void MenuSelectControl_HideControl()
        {
            MenuSelectControl.Visibility = Visibility.Collapsed;
            DataListView.ItemsSource = null;
            DataListView.ItemsSource = App.tableViewModel.Items;
        }
    }
}