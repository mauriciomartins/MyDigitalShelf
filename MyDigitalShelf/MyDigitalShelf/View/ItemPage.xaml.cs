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
    {   private ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
        private string id;

        public MainPage(string UserId)
        {
            InitializeComponent();
            this.ItemDirectoryVM.UserId = UserId;
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
            Navigation.PushAsync(new View.ItemDetail(this.ItemDirectoryVM.UserId, newItem), true);
        }


        private void UpdateItem_Clicked(object sender, EventArgs e)
        {
            Item selectedItem = (Item)this.MyItemList.Item;

            if (selectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new View.ItemDetail(this.ItemDirectoryVM.UserId, selectedItem), true);
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
            this.ItemDirectoryVM.Remove(item);
        }

        public void AppedItem(Item item)
        {
            this.ItemDirectoryVM.Add(item);
        }
    }
}
