using MyDigitalShelf.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDigitalShelf.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFriendItems : ContentPage
    {
        private ItemDirectoryVM ItemDirectoryVM;
        public MyFriendItems(ItemDirectoryVM ItemDirectoryVM)
        {
            InitializeComponent();
            this.ItemDirectoryVM = ItemDirectoryVM;
            this.BindingContext = this.ItemDirectoryVM;
            this.Refresh();

        }

       
        public void Refresh()
        {
            ItemDirectoryVM.LoadDirectory();
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
