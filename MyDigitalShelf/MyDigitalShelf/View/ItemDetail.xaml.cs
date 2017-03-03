using MyDigitalShelf.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDigitalShelf.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetail : ContentPage
    {
        private bool isNew;
        private ItemDirectoryVM itemDirectoryVM = new ItemDirectoryVM();
        public ItemDetail(string userId,Item item)
        {
            InitializeComponent();
            this.itemDirectoryVM.UserId = userId;
            this.itemDirectoryVM.Item   = item;
            this.isNew = (item.Id == null || item.Id.Length == 0);
            this.BindingContext = itemDirectoryVM;
            this.SaveButton.Clicked         += SaveButton_Clicked;
            this.SearchItemButton.Clicked   += SearchItemButton_Clicked;
            this.DeleteButton.Clicked       += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (itemDirectoryVM.Item.Id == null || itemDirectoryVM.Item.Id.Length == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Item não cadastrado!", "OK");
            }
            else
            {
                itemDirectoryVM.DeleteData();
                Page page = await Navigation.PopAsync();
                NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                ((MainPage)navPage.CurrentPage).RemoveItem(this.itemDirectoryVM.Item);
            }
        }

        private void SearchItemButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchItemPage(itemDirectoryVM.Item));
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (this.ItemName.Text==null||this.ItemName.Text.Length==0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nenhum nome foi informado!", "OK");
            }
            else if (this.ItemDescription.Text == null || this.ItemDescription.Text.Length == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nenhuma descrição foi informada", "OK");
            }
            else
            {
                itemDirectoryVM.SaveData();
                Page page = await Navigation.PopAsync();
                if (this.isNew)
                {
                    NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                    ((MainPage)navPage.CurrentPage).AppedItem(this.itemDirectoryVM.Item);
                }
            }
        }
    }
}
