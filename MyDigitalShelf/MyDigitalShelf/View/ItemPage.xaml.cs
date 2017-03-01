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
    {
        ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
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
            Navigation.PushAsync(new View.ItemDetail(newItem), true);
        }


        private void UpdateItem_Clicked(object sender, EventArgs e)
        {
            Item selectedItem = (Item)this.MyItemList.Item;

            if (selectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new View.ItemDetail(selectedItem), true);
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Debug.WriteLine("{0} MainPage.OnDisappearing", Title);
        }
    
       
    }
}
