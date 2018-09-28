using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KimBab.ViewModel
{
    class StatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Menu> Items { get; set; }

        private bool IsDataLoaded { get; set; }

        public StatisticsViewModel()
        {
            Items = new ObservableCollection<Menu>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
