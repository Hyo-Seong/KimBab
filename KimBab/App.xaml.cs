using KimBab.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KimBab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MenuViewModel menuViewModel = new MenuViewModel();
        public static TableViewModel tableViewModel = new TableViewModel();
    }
}
