using KimBab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace KimBab.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Menu> Items { get; } = new ObservableCollection<Menu>();

        //public List<Menu>  
        
        public long pepsi = 8801056070809;
        public long coca = 8801094017200;

        private bool IsDataLoaded { get; set; }

        public MenuViewModel()
        {
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public void AddStatistics(int tableNum)
        //{
        //    foreach (Menu menu in App.tableViewModel.Items[tableNum].MenuList)
        //    {
        //        try
        //        {
        //            Items[menu.Idx].Orders += menu.Orders;
        //            Items[menu.Idx].OrderPrice += menu.Price;
        //        }
        //        catch (Exception exception)
        //        {
        //            Debug.WriteLine(exception.Message);
        //        }
        //    }
        //    App.tableViewModel.ClearTable(tableNum);
        //}

        private Menu CopyMenu(Menu menu)
        {
            return new Menu
            {
                Price = menu.Price,
                Type = menu.Type,
                Image = menu.Image,
                Name = menu.Name,
                OrderPrice = menu.OrderPrice,
                Orders = 1,
                Barcode = menu.Barcode,
            };
        }

        public void LoadData()
        {
            Items.Add(new Menu { Name = "김밥", Price = 1500, Type = FoodType.KIMBAB });
            Items.Add(new Menu { Name = "치즈김밥", Price = 2500, Type = FoodType.KIMBAB });
            Items.Add(new Menu { Name = "참치김밥", Price = 2500, Type = FoodType.KIMBAB });
            Items.Add(new Menu { Name = "땡초김밥", Price = 2500, Type = FoodType.KIMBAB });
            Items.Add(new Menu { Name = "돈까스김밥", Price = 2500, Type = FoodType.KIMBAB });
            Items.Add(new Menu { Name = "떡볶이", Price = 3000, Type = FoodType.BUNSIK });
            Items.Add(new Menu { Name = "라볶이", Price = 4500, Type = FoodType.BUNSIK });
            Items.Add(new Menu { Name = "떡만두국", Price = 4500, Type = FoodType.BUNSIK });
            Items.Add(new Menu { Name = "비빔만두", Price = 4500, Type = FoodType.BUNSIK });
            Items.Add(new Menu { Name = "제육덮밥", Price = 5000, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "쇠고기덮밥", Price = 5000, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "김치덮밥", Price = 4500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "비빔밥", Price = 4500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "돌솥비빔밥", Price = 5000, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "갈비탕", Price = 5500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "육개장", Price = 5500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "된장찌개", Price = 4500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "순두부찌개", Price = 4500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "참치찌개", Price = 4500, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "공깃밥", Price = 1000, Type = FoodType.SIKSA });
            Items.Add(new Menu { Name = "라면", Price = 3000, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "짬뽕라면", Price = 4000, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "우동", Price = 3000, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "쫄면", Price = 4000, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "잔치국수", Price = 4000, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "비빔국수", Price = 4500, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "칼국수", Price = 4500, Type = FoodType.NOODLE });
            Items.Add(new Menu { Name = "돈까스", Price = 5000, Type = FoodType.DONGAS });
            Items.Add(new Menu { Name = "치즈돈까스", Price = 5500, Type = FoodType.DONGAS });
            Items.Add(new Menu { Name = "고구마돈까스", Price = 6000, Type = FoodType.DONGAS });

            Items.Add(new Menu { Name = "펩시", Price = 800, Type = FoodType.DRINK, Barcode = 8801056070809 });
            Items.Add(new Menu { Name = "코카콜라", Price = 2000, Type = FoodType.DRINK, Barcode = 8801094017200 });
            SetIdx();
        }

        public void SetIdx()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Idx = i;
            }
        }
    }
}