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
    public partial class ItemDetail : ContentPage
    {
        public ItemDetail(Item item)
        {
            InitializeComponent();
            ItemDirectoryVM itemDirectoryVM = new ItemDirectoryVM();
            itemDirectoryVM.Item = item;
            this.BindingContext = itemDirectoryVM; 
        }
    }
}
