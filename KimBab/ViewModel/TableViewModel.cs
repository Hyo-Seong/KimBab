using KimBab.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace KimBab.ViewModel
{
    public class TableViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Table> Items { get; set; }

        private bool IsDataLoaded { get; set; }

        public TableViewModel()
        {
            Items = new ObservableCollection<Table>();
            LoadData();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
        public void OrderMenu(int index, Menu menu)
        {
            try
            {
                for(int i=0; i<Items[index].Menu.Count; i++)
                {
                    if (Items[index].Menu[i].Name.Equals(menu.Name))
                    {
                        Items[index].Menu[i].Orders++;
                        Debug.WriteLine("0 " + menu.Name + " " + menu.Orders);
                        return;
                    }
                }
                Debug.WriteLine("1 " + menu.Name + " " + menu.Orders);
                Items[index].Menu.Add(menu);
            } catch
            {
                Debug.WriteLine("2 " + menu.Name + " " + menu.Orders);
                Items[index].Menu.Add(menu);
            }
        }

        public void LoadData()
        {
            Items.Add(new Table(1));
            Items.Add(new Table(2));
            Items.Add(new Table(3));
            Items.Add(new Table(4));
            Items.Add(new Table(5));
            Items.Add(new Table(6));
            Items.Add(new Table(7));
            Items.Add(new Table(8));
            Items.Add(new Table(9));
        }
    }
}
