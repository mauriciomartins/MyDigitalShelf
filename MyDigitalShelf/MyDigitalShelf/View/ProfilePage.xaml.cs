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
    public partial class ProfilePage : ContentPage
    {
        private ProfileVM ProfileVM = new ProfileVM();
        public ProfilePage(User User)
        {
            InitializeComponent();
            this.ProfileVM.User = User;
            this.BindingContext = ProfileVM;
            this.LogoutButton.Clicked += LogoutButton_Clicked;
            this.EditButton.Clicked   += EditButton_Clicked;
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            bool isNew = false;
            await Navigation.PushAsync(new SignUpPage(isNew, this.ProfileVM.User));
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            GoBack();
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
