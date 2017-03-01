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
    public partial class NotesPage : ContentPage
    {
         public NotesPage(Item item)
        {
            InitializeComponent();
            ContentDirectoryVM contentDirectoryVM = new ContentDirectoryVM();
            this.BindingContext = contentDirectoryVM;
            contentDirectoryVM.LoadDirectory();
        }
    }
}
