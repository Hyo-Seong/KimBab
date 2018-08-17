using KimBab.Model;
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
    }
}