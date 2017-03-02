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
        private ItemDirectoryVM itemDirectoryVM = new ItemDirectoryVM();
        public ItemDetail(Item item)
        {
            InitializeComponent();
            itemDirectoryVM.Item = item;
            this.BindingContext = itemDirectoryVM;
            this.SaveButton.Clicked         += SaveButton_Clicked;
            this.SearchItemButton.Clicked   += SearchItemButton_Clicked;
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
                await Task.Delay(3000);
                Page page = await Navigation.PopAsync();
                NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                ((MainPage)navPage.CurrentPage).Refresh();
            }
        }
    }
}
