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
        private ItemDirectoryVM ItemDirectoryVM = new ItemDirectoryVM();
        public Home(string UserId)
        {
            InitializeComponent();
            this.ItemDirectoryVM.UserId = UserId;
            this.BindingContext = ItemDirectoryVM;
            this.itemPage = new MainPage(ItemDirectoryVM);
            this.searchItemPage = new SearchItemPage(UserId, ItemDirectoryVM.ItemList);
            this.Children.Add(
                itemPage
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
