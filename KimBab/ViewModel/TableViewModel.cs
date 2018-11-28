using KimBab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace KimBab.ViewModel
{
    public class TableViewModel
    {
        public ObservableCollection<Table> Items { get; set; }

        public List<Table> statistics = new List<Table>();

        public Table TempTable;

        private bool IsDataLoaded { get; set; }

        public TableViewModel()
        {
            Items = new ObservableCollection<Table>();
            LoadData();
        }

        public void AddStatistics(int tableNum)
        {
            statistics.Add(CopyTable(Items[tableNum]));
            ClearTable(tableNum);
        }

        public void AddOrderMenu(int tableNum, Menu menu)
        {
            try
            {
                for (int i = 0; i < Items[tableNum].MenuList.Count; i++)
                {
                    if (Items[tableNum].MenuList[i].Name.Equals(menu.Name))
                    {
                        Items[tableNum].MenuList[i].Orders += 1;
                        Items[tableNum].TotalPrice += menu.Price;
                        return;
                    }
                }
				Items[tableNum].MenuList.Add(CopyMenu(menu));

				Items[tableNum].TotalPrice += menu.Price;
				SortMenuList(tableNum);
			}
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }

        }

        public void SetTempItems(int tableNum)
        {
            TempTable = new Table(); 
            TempTable = CopyTable(Items[tableNum]);
        }

        private Table CopyTable(Table table)
        {
            List<Menu> menus = new List<Menu>();
            foreach(Menu menu in table.MenuList)
            {
                menus.Add(menu);
            }

            return new Table
            {
                TableNum = table.TableNum,
                MenuList = menus,
                MenuString = table.MenuString,
                OrderDateTime = table.OrderDateTime,
                TotalPrice = table.TotalPrice,
                Payment = table.Payment,
            };
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
                Barcode = menu.Barcode
            };
        }

        public void ClearTable(int tableNum)
        {
            Items[tableNum].MenuList.Clear();
            Items[tableNum].TotalPrice = 0;
            Items[tableNum].MenuString = "";
            Items[tableNum].OrderDateTime = "주문시간 : ";
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
            foreach (Menu menu in Items[tableNum].MenuList)
            {
                Items[tableNum].MenuString += menu.Name + "  " + menu.Orders + "\n";
            }
        }

        public void CancelMenu(int tableNum, int selectedIndex)
        {
            Items[tableNum].TotalPrice -= Items[tableNum].MenuList[selectedIndex].OrderPrice;
            Items[tableNum].MenuList.RemoveAt(selectedIndex);
        }

        public void CancelOrder(int tableNum)
        {
            Items[tableNum] = CopyTable(TempTable);
        }

        public void ClearList(int tableNum)
        {
            Items[tableNum].MenuList.Clear();
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
            if (Items[tableNum].MenuList[selectedIndex].Orders <= 0)
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

        public void SetOrderDateTime(int tableNum, bool isNull)
        {
            string timeString = "주문시간 : ";
            if (!isNull)
            {
                timeString += DateTime.Now.ToString();
            }
            Items[tableNum].OrderDateTime = timeString;
        }

        public bool CheckIsChanged(int tableNum)
        {
            if(Items[tableNum].MenuString == TempTable.MenuString)
            {
                return true;
            }
            return false;
        }
    }
}