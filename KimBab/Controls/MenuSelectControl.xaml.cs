using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class MenuSelectControl : UserControl
    {
        public MenuSelectControl()
        {
            InitializeComponent();
            
        }

        private int index;

        public void SetItemIndex(int index)
        {
            Debug.WriteLine(index);
            this.index = index;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
