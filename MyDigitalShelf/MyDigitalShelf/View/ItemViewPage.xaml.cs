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
    public partial class ItemViewPage : ContentPage
    {
        private bool isNew;
        private ItemDirectoryVM itemDirectoryVM = new ItemDirectoryVM();
        public ItemViewPage(string userId, Item item)
        {
            InitializeComponent();
            this.itemDirectoryVM.UserId = userId;
            this.itemDirectoryVM.Item = item;
            this.isNew = (item.Id == null || item.Id.Length == 0);
            this.BindingContext = itemDirectoryVM;
            this.UpdateItem.Clicked += UpdateItem_Clicked;
        }

        private void Show_My_Note(object sender, EventArgs e)
        {
            Navigation.PushAsync(new View.NotesPage(this.itemDirectoryVM.Item), true);
        }

        private void UpdateItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new View.ItemDetail(this.itemDirectoryVM.UserId, this.itemDirectoryVM.Item), true);
        }
    }
}
