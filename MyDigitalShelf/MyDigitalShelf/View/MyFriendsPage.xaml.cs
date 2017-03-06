using MyDigitalShelf.model;
using MyDigitalShelf.ViewModel;
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
    public partial class MyFriendsPage : ContentPage
    {
        private MyFriendVM MyFriendVM = new MyFriendVM();
        public MyFriendsPage(User User)
        {
            InitializeComponent();
            this.MyFriendVM.UserId = User.Id;
            this.BindingContext = MyFriendVM;
            this.LogoutButton.Clicked += LogoutButton_Clicked;
            this.Refresh();
        }

        public async void Refresh()
        {
             MyFriendVM.SearchMyFriends();
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            GoBack();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            User selectedItem = (User)this.ItemList.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }
            ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
            ItemDirectoryVM.IsMine          = false;
            ItemDirectoryVM.UserId          = selectedItem.Id;
            ItemDirectoryVM.User            = selectedItem;
            Navigation.PushAsync(new MyFriendItems(ItemDirectoryVM), true);
            this.ItemList.SelectedItem = null;
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
