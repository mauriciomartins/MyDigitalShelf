using MyDigitalShelf.model;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.ViewModel
{
    class MyFriendVM : ObservableBaseObject
    {
        private string UserName = "";
        private string userId;
        private bool isBusy = false;
        private bool isEmpty = false;
        private UserDirectoryService userDirectoryService;
        private string email;
        private string password;
        public ObservableCollection<User> MyFriendList { get; set; }
        public MyFriendVM()
        {
            this.MyFriendList         = new ObservableCollection<User>();
            this.userDirectoryService = new UserDirectoryService();
            this.IsBusy               = false;
            this.SearchFriendsCommand = new Command(() => SearchMyFriends(), () => !this.IsBusy);
            
        }

        public Command SearchFriendsCommand
        {
            get; set;
        }

        public async void SearchMyFriends()
        {
            if (!IsBusy)
            {

                try
                {
                    IsBusy = true;
                    if (this.MyFriendList != null && this.MyFriendList.Any() && this.MyFriendList.Count > 0)
                    {
                        this.RemoveAll(this.MyFriendList);
                        await this.userDirectoryService.CleanData();
                    }

                    var users = await this.userDirectoryService.GetUsers(this.UserId, UserName.Replace(" ", "+"));
                    if (users.Any())
                    {
                        foreach (var User in users)
                        {
                            this.MyFriendList.Add(User);
                        }
                    }
                    IsEmpty = this.MyFriendList == null || this.MyFriendList.Count == 0;
                }
                catch (Exception e)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", e.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

        }


        private void RemoveAll(ObservableCollection<User> itemList)
        {
            while (itemList.Count > 0)
            {
                itemList.RemoveAt(itemList.Count - 1);
            }
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public bool IsEmpty
        {
            get { return this.isEmpty; }
            set { this.isEmpty = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; OnPropertyChanged(); }
        }

        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; OnPropertyChanged(); }
        }
    }
}
