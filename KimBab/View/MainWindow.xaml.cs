﻿using KimBab.Model;
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

        private void SetLoginProgressRing(bool isActive)
        {
            LoadingControl.Visibility = isActive ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DataListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Table data = (Table)DataListView.SelectedItem;
            if (data != null)
            {
                MenuSelectControl.SetItemIndex(data.TableNum - 1);
                MenuSelectControl.Visibility = Visibility.Visible;

                Debug.WriteLine(data.TableNum);
            }
        }
    }
}