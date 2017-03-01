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
            NotesDirectoryVM notesDirectoryVM = new NotesDirectoryVM();
            this.BindingContext = notesDirectoryVM;
            notesDirectoryVM.LoadDirectory(item.Id);
        }
    }
}
