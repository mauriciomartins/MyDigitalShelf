using MyDigitalShelf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static async Task<bool?> CallHardwareBackPressed()
        {
            Func<Task<bool?>> backPressed = HardwareBackPressed;
            if (backPressed != null)
            {
                return await backPressed();
            }

            return true;
        }

        public static Func<Task<bool?>> HardwareBackPressed
        {
            private get;
            set;
        }

        protected virtual void OnAppearing()
        {
            App.HardwareBackPressed = () => Task.FromResult<bool?>(null);
        }
        
        protected virtual void OnDisappearing()
        {
            App.HardwareBackPressed = null;
        }
    }
}
