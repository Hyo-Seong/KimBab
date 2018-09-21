using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab.Model
{
    public enum FoodType
    {
        KIMBAB,
        NOODLE,
        SIKSA,
        BUNSIK,
        DONGAS
    }

    public class Menu : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                image = "pack://application:,,,/resource/menu/" + name + ".jpg";
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Image");
            }
        }

        private string image;
        public string Image
        {
            get => image;
            set
            {
                image = value;
            }
        }

        private int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;
                NotifyPropertyChanged("Price");
            }
        }

        private int orderPrice = 0;
        public int OrderPrice
        {
            get => orderPrice;
            set
            {
                orderPrice += price;
                NotifyPropertyChanged("OrderPrice");
            }
        }
        private int orders;
        public int Orders
        {
            get => orders;
            set
            {
                orders = value;
                orderPrice = price * orders;
            }
        }

        private FoodType type;
        public FoodType Type
        {
            get => type;
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }
    }
}
