using KimBab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Menu> Items { get; set; }

        private bool IsDataLoaded { get; set; }

        public MenuViewModel()
        {
            Items = new ObservableCollection<Menu>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddMenu(Menu menu)
        {
            ////Items.Add(menu);
            //Items.Add(new Menu
            //{
            //    Name = "GI",
            //    Price = 40000,
            //    Type = 0
            //});
            //Items.Add(new Menu
            //{
            //    Name = "GI",
            //    Price = 40000,
            //    Type = 0
            //});        
}
    }
}
