using MyDigitalShelf.model;
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
    public partial class NotesPage : ContentPage
    {
        private NotesDirectoryVM notesDirectoryVM = new NotesDirectoryVM();
         public NotesPage(Item item)
        {
            InitializeComponent();
            this.notesDirectoryVM.Item   = item;
            this.notesDirectoryVM.IsEmptyImage = item.Image == null || item.Image.Length == 0; 
            this.notesDirectoryVM.ItemId = item.Id;
            this.BindingContext   = notesDirectoryVM;
            this.AddNote.Clicked += AddItem_Clicked;
            this.UpdateNote.Clicked += UpdateNote_Clicked;
            this.Refresh();
        }

        public void Refresh()
        {
            notesDirectoryVM.LoadDirectory();
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            Notes newNote = new Notes();
            Navigation.PushAsync(new View.NotesDetailPage(this.notesDirectoryVM.ItemId, newNote), true);
        }


        private void UpdateNote_Clicked(object sender, EventArgs e)
        {
            Notes selectedItem = (Notes)this.NotesList.Item;

            if (selectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new View.NotesDetailPage(this.notesDirectoryVM.ItemId, selectedItem), true);
        }

        public void RemoveNote(Notes notes)
        {
            this.notesDirectoryVM.Remove(notes);
        }

        public void AppenNote(Notes notes)
        {
            this.notesDirectoryVM.Add(notes);
         }
    }
}
