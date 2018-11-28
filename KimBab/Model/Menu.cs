using System.ComponentModel;

namespace KimBab.Model
{
    public enum FoodType
    {
        KIMBAB,
        NOODLE,
        SIKSA,
        BUNSIK,
        DONGAS,
        DRINK
    }

    public class Menu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int idx = -1;

        public int Idx
        {
            get => idx;
            set
            {
                idx = value;
            }
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
                NotifyPropertyChanged("OrderPrice");
            }
        }

        private int orders = 0;

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

        private string barcode = "0";

        public string Barcode
        {
            get => barcode;
            set
            {
                barcode = value;
                NotifyPropertyChanged("Barcode");
            }
        }
    }
}