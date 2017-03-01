using MyDigitalShelf.model;
using MyDigitalShelf.MyDigitalShelf;
using MyDigitalShelf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
            this.BindingContext = ItemDirectoryVM;
            this.ItemList.ItemSelected += ItemList_ItemSelected;
            this.AddItem.Clicked += AddItem_Clicked;
            ItemDirectoryVM.LoadDirectory();
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new View.CategoryInfoPage());
        }

        private void ItemList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Item selectedItem = (Item)e.SelectedItem;
            if(selectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new View.Content(null));
            this.ItemList.SelectedItem = null;
        }
    }
}
