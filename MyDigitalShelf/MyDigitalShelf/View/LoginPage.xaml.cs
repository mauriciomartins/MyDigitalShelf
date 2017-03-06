using MyDigitalShelf.model;
using MyDigitalShelf.ViewModel;
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
    public partial class LoginPage : ContentPage
    {
        private LoginVM loginVM = new LoginVM();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = loginVM;
            this.GoButton.Clicked += GoButton_clicked;
        }

        private async void GoButton_clicked(object sender, EventArgs e)
        {
            User user = await loginVM.LoginDirectory();
            if (user != null)
            {
                Home home = new View.Home(user);
                await Navigation.PushAsync(home);
                Navigation.RemovePage(this);
            }
        }
    }
}
