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
    public partial class Home : TabbedPage
    {
        private Page itemPage;
        private SearchItemPage searchItemPage;
        private MyFriendsPage MyFriendsPage;
        private ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
        private ProfilePage ProfilePage;

        public Home(User User)
        {
            InitializeComponent();
            this.ItemDirectoryVM.UserId = User.Id;
            this.ItemDirectoryVM.IsMine = true;
            this.BindingContext = ItemDirectoryVM;
            this.ProfilePage    = new ProfilePage(User);
            this.itemPage       = new MainPage(ItemDirectoryVM);
            this.MyFriendsPage  = new MyFriendsPage(User);
            this.searchItemPage = new SearchItemPage(User.Id, ItemDirectoryVM.ItemList);
            this.Children.Add(
               ProfilePage
           );

            this.Children.Add(
                itemPage
            );
           
            this.Children.Add(
                MyFriendsPage
            );
            this.Children.Add(
                searchItemPage
            );
        }

        public void RemoveItem(Item item)
        {
            this.ItemDirectoryVM.Remove(item);
        }

        public void AppedItem(Item item)
        {
            this.ItemDirectoryVM.Add(item);
        }
    }
}
