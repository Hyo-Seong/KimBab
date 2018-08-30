using System.Collections.Generic;
using System.ComponentModel;

namespace KimBab.Model
{
    public class Table : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int tableNum;
        public int TableNum
        {
            get => tableNum;
            set
            {
                tableNum = value;
                NotifyPropertyChanged("TableNum");
            }
        }

        private List<Menu> menu;
        public List<Menu> Menu
        {
            get => menu;
            set
            {
                menu = value;
                NotifyPropertyChanged("Menu");
            }
        }

        private int totalPrice;

        public int TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
        }

        public Table(int TableNum)
        {
            tableNum = TableNum;
            menu = new List<Menu>();
        }
    }
}