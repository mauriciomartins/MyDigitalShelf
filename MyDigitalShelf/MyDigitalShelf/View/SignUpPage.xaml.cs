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
    public partial class SignUpPage : ContentPage
    {
        private ProfileVM ProfileVM = new ProfileVM();
        private bool isNew;
        public SignUpPage(bool isNew,User user)
        {
            InitializeComponent();
            this.isNew          = isNew;
            this.ProfileVM.User = user;
            this.BindingContext = ProfileVM;
            this.SaveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (this.Name.Text == null || this.Name.Text.Length == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nenhum nome foi informado!", "OK");
            }
            else if (this.Email.Text == null || this.Email.Text.Length == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nenhuma e-mail foi informada", "OK");
            }
            else
            {
                ProfileVM.SaveData();
                await Navigation.PopToRootAsync();
            }
        }
    }
}
