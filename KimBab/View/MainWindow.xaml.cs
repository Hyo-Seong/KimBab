using KimBab.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

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
            
            Debug.WriteLine(DataListView.ItemsSource);
        }

        private void DataListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Table data = (Table)DataListView.SelectedItem;
            if(data != null)
            {
                MenuSelectControl.SetItemIndex(data.TableNum - 1);
                MenuSelectControl.Visibility = Visibility.Visible;

                Debug.WriteLine(data.TableNum);
            }
            
        }
    }
}