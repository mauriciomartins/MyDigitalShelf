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
        ItemDirectoryVM itemDirectoryVM = new ItemDirectoryVM();
        public ItemDetail(Item item)
        {
            InitializeComponent();
            itemDirectoryVM.Item = item;
            this.BindingContext = itemDirectoryVM;
            this.SaveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            itemDirectoryVM.SaveData();
            await Task.Delay(1000);
            Page page = await Navigation.PopAsync();
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((MainPage)navPage.CurrentPage).Refresh();
        }
    }
}
