using System.Collections.Generic;
using System.ComponentModel;

namespace KimBab.Model
{
    public enum PaymentType
    {
        CASH,CARD
    }

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

        private List<Menu> menuList;

        public List<Menu> MenuList
        {
            get => menuList;
            set
            {
                menuList = value;
                NotifyPropertyChanged("Menu");
            }
        }

        private string menuString;

        public string MenuString
        {
            get => menuString;
            set
            {
                menuString = value;
                NotifyPropertyChanged("MenuString");
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

        private string orderDateTime = "주문시간 : ";

        public string OrderDateTime
        {
            get => orderDateTime;
            set
            {
                orderDateTime = value;
                NotifyPropertyChanged("OrderDateTime");
            }
        }

        private PaymentType payment;
        public PaymentType Payment
        {
            get => payment;
            set
            {
                payment = value;
                NotifyPropertyChanged("Payment");
            }
        }

        public Table(int TableNum)
        {
            Payment = PaymentType.CASH;
            this.TableNum = TableNum;
            MenuList = new List<Menu>();
        }

        public Table()
        {
            MenuList = new List<Menu>();
        }
    }
}