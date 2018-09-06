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
    
        public void AddOrderMenu(int tableNum, Menu menu)
        {
            try
            {
                for(int i=0; i<Items[tableNum].Menu.Count; i++)
                {
                    if (Items[tableNum].Menu[i].Name.Equals(menu.Name))
                    {
                        Debug.WriteLine("0 " + menu.Name + " " + menu.Orders);
                        Items[tableNum].Menu[i].Orders++;
                        Items[tableNum].TotalPrice += menu.Price;
                        Debug.WriteLine("0 " + menu.Name + " " + menu.Orders);
                        return;
                    }
                }

            } catch
            {
            }
            Debug.WriteLine("1 " + menu.Name + " " + menu.Orders);

            Items[tableNum].Menu.Add(menu);
            Debug.WriteLine("1 " + menu.Name + " " + menu.Orders);
            Items[tableNum].TotalPrice += menu.Price;
            SortMenuList(tableNum);
        }

        private void SortMenuList(int tableNum)
        {
            Items[tableNum].Menu.Sort(delegate (Menu x, Menu y)
            {
                if (x.Name == null && y.Name == null) return 0;
                else if (x.Name == null) return -1;
                else if (y.Name == null) return 1;
                else return x.Name.CompareTo(y.Name);
            });
        }

        public void SetMenuString(int tableNum)
        {
            Items[tableNum].MenuString = "";
            foreach(Menu menu in Items[tableNum].Menu)
            {
                Items[tableNum].MenuString += menu.Name + "  " + menu.Orders + "\n";
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