using MyDigitalShelf.model;
using MyDigitalShelf.MyDigitalShelf;
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
    public partial class Content : ContentPage
    {
        public Content(Category selectedCategory)
        {
            InitializeComponent();
            ContentDirectoryVM contentDirectoryVM = new ContentDirectoryVM();
            this.BindingContext = contentDirectoryVM;
            contentDirectoryVM.LoadDirectory();
        }
    }
}
