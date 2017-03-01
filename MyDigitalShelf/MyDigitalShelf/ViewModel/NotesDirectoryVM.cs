using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.model
{
    class NotesDirectoryVM : ObservableBaseObject
    {
        public ObservableCollection<Notes> NotesList { get; set; }
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public Command LoadContentDirectoryCommand
        {
            get; set;
        }

        public NotesDirectoryVM()
        {
            this.NotesList = new ObservableCollection<Notes>();
            this.IsBusy = false;
          //  this.LoadContentDirectoryCommand = new Command(() => LoadDirectory(),()=>!this.IsBusy);
        }

        public async void LoadDirectory(string itemId)
        {
            if (!IsBusy)
            {
                IsBusy = true;

               
                var loadDirectory = await new NotesDirectoryService().GetNotes(itemId);

                foreach (var note in loadDirectory)
                {
                    this.NotesList.Add(note);
                }

                IsBusy = false;
            }
        }
    }
}
