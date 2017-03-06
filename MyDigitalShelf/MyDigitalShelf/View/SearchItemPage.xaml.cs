using MyDigitalShelf.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public SearchItemPage(string userId, ObservableCollection<Item> ItemList)
        {
            InitializeComponent();
            this.ItemDirectoryVM.UserId   = userId;
            this.ItemDirectoryVM.ItemList = ItemList;
            this.ItemDirectoryVM.IsMine   = true;
            this.BindingContext           = ItemDirectoryVM;
            this.LogoutButton.Clicked += LogoutButton_Clicked;
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
            selectedItem.UserId   = this.ItemDirectoryVM.UserId;
            selectedItem.Position = this.ItemDirectoryVM.LastPosition();
            selectedItem.Date     = DateTime.Today.Year.ToString();
            Navigation.PushAsync(new View.ItemViewPage(this.ItemDirectoryVM.UserId, selectedItem), true);
            this.ItemList.SelectedItem = null;
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            GoBack();
        }

        internal void RemoveItem(Item item)
        {
            this.ItemDirectoryVM.Remove(item);
        }

        internal void AppedItem(Item item)
        {
            this.ItemDirectoryVM.Add(item);
        }

        protected override bool OnBackButtonPressed()
        {
            this.GoBack();

            return true;
        }

        private async void GoBack()
        {
            var answer = await DisplayAlert("Logout", "Would you like to logout?", "Yes", "Cancel");
            if (answer)
            {
                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new LoginPage());
                Navigation.RemovePage((Home)this.Parent);
            }
        }
    }
}
