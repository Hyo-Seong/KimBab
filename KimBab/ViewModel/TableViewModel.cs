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
                for(int i=0; i<Items[tableNum].MenuList.Count; i++)
                {
                    if (Items[tableNum].MenuList[i].Name.Equals(menu.Name))
                    {
                        Items[tableNum].MenuList[i].Orders += 1;
                        Items[tableNum].TotalPrice += menu.Price;
                        return;
                    }
                }

            } catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            Items[tableNum].MenuList.Add(CopyMenu(menu));

            Items[tableNum].TotalPrice += menu.Price;
            SortMenuList(tableNum);
        }

        private Menu CopyMenu(Menu menu)
        {
            return new Menu
            {
                Idx = menu.Idx,
                Price = menu.Price,
                Type = menu.Type,
                Image = menu.Image,
                Name = menu.Name,
                OrderPrice = menu.OrderPrice,
                Orders = 1,
            };
        }

        public void ClearTable(int tableNum)
        {
            Items[tableNum].MenuList.Clear();
            Items[tableNum].TotalPrice = 0;
            Items[tableNum].MenuString = "";
        }

        private void SortMenuList(int tableNum)
        {
            Items[tableNum].MenuList.Sort(delegate (Menu x, Menu y)
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
            foreach(Menu menu in Items[tableNum].MenuList)
            {
                Items[tableNum].MenuString += menu.Name + "  " + menu.Orders + "\n";
            }
        }

        public void CancelMenu(int tableNum, int selectedIndex)
        {
            Items[tableNum].TotalPrice -= Items[tableNum].MenuList[selectedIndex].OrderPrice;
            //totalprice 설정
            Items[tableNum].MenuList.RemoveAt(selectedIndex);
        }

        public void ClearList(int tableNum)
        {
            Items[tableNum].MenuList.Clear();
            Debug.WriteLine("A" + Items[tableNum].TableNum);
            Items[tableNum].TotalPrice = 0;
        }

        public void PlusOrder(int tableNum, int selectedIndex)
        {
            Items[tableNum].MenuList[selectedIndex].Orders++;
            Items[tableNum].TotalPrice += Items[tableNum].MenuList[selectedIndex].Price;
        }

        public void MinusOrder(int tableNum, int selectedIndex)
        {
            Items[tableNum].MenuList[selectedIndex].Orders--;
            Items[tableNum].TotalPrice -= Items[tableNum].MenuList[selectedIndex].Price;
            if(Items[tableNum].MenuList[selectedIndex].Orders <= 0)
            {
                Items[tableNum].MenuList.RemoveAt(selectedIndex);
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