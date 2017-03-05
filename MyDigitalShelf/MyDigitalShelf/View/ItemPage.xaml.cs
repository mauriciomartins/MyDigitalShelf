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
            this.Refresh();
        }
                
        public  void Refresh()
        {
              ItemDirectoryVM.LoadDirectory();
        }

        private void AddItem_Clicked(object sender, EventArgs e)//
        {
            Item newItem = new Item();
            newItem.Position = this.ItemDirectoryVM.LastPosition();
            newItem.Date = DateTime.Today.Year.ToString();
            Navigation.PushAsync(new View.ItemDetail(this.ItemDirectoryVM.UserId, newItem), true);
           
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Item selectedItem = (Item)this.MyItemList.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }
            Navigation.PushAsync(new View.ItemViewPage(this.ItemDirectoryVM.UserId, selectedItem), true);
            this.MyItemList.SelectedItem = null;
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
