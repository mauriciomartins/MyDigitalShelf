using MyDigitalShelf.model;
using MyDigitalShelf.MyDigitalShelf;
using MyDigitalShelf.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf
{
    public partial class MainPage : ContentPage
    {   private string userId = "df258d04-d3da-4380-a528-113d34d9e26c";
        private ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = ItemDirectoryVM;
            this.AddItem.Clicked += AddItem_Clicked;
            this.UpdateItem.Clicked += UpdateItem_Clicked;
            this.Refresh();
        }

        public  void Refresh()
        {
              ItemDirectoryVM.LoadDirectory();
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            Item newItem = new Item();
            newItem.Date = DateTime.Today.Year.ToString();
            Navigation.PushAsync(new View.ItemDetail(userId,newItem), true);
        }


        private void UpdateItem_Clicked(object sender, EventArgs e)
        {
            Item selectedItem = (Item)this.MyItemList.Item;

            if (selectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new View.ItemDetail(userId,selectedItem), true);
        }


        private void ItemList_ItemSelected(object sender, EventArgs e)
        {
            Item selectedItem = (Item)this.MyItemList.Item;

            if (selectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new View.NotesPage(selectedItem), true);
        }

        public void RemoveItem(Item item)
        {
            this.ItemDirectoryVM.ItemList.Remove(item);
        }

        public void AppedItem(Item item)
        {
            this.ItemDirectoryVM.ItemList.Add(item);
        }
    }
}
