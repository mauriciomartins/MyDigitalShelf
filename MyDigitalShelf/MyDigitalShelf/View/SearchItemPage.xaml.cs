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
    public partial class SearchItemPage : ContentPage
    {
        ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
        public SearchItemPage(Item item)
        {
            InitializeComponent();
            this.BindingContext  = ItemDirectoryVM;
            this.SelectItemButton.Clicked += SelectItemButton_Clicked;
            this.ItemDirectoryVM.Item = item;
        }

        private async  void SelectItemButton_Clicked(object sender, EventArgs e)
        {
            Item selectedItem = (Item)this.ItemList.SelectedItem;

            if (selectedItem == null)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Nenhum item foi selecionado!", "OK");
            }
            else
            {
                this.ItemDirectoryVM.Item.Name              = selectedItem.Name;
                this.ItemDirectoryVM.Item.Description       = selectedItem.Description;
                this.ItemDirectoryVM.Item.Publishingcompany = selectedItem.Publishingcompany;
                this.ItemDirectoryVM.Item.PublishingDate    = selectedItem.PublishingDate;
                this.ItemDirectoryVM.Item.Writer            = selectedItem.Writer;
                this.ItemDirectoryVM.Item.Image             = selectedItem.Image;
                this.ItemDirectoryVM.Item.Source            = selectedItem.Source;
                this.ItemDirectoryVM.Item.Link              = selectedItem.Link;
                Page page = await Navigation.PopAsync();
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Item selectedItem = (Item)this.ItemList.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }
            Navigation.PushModalAsync(new View.ItemViewPage(this.ItemDirectoryVM.UserId, selectedItem), true);
            
        }
    }
}
