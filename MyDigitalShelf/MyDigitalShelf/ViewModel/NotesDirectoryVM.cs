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

namespace MyDigitalShelf.model
{
    class NotesDirectoryVM : ObservableBaseObject
    {
        private Notes notes;
        private string itemId;
        private NotesDirectoryService      notesDirectoryService;
        public ObservableCollection<Notes> NotesList { get; set; }
        private bool isBusy = false;
        private bool isEmpty = false;
        private Item item;

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
        public Command LoadContentDirectoryCommand
        {
            get; set;
        }

        public NotesDirectoryVM()
        {
            this.notesDirectoryService = new NotesDirectoryService();
            this.NotesList             = new ObservableCollection<Notes>();
            this.IsBusy = false;
            this.LoadContentDirectoryCommand = new Command(() => LoadDirectory(),()=>!this.IsBusy);
        }


        public Notes Notes
        {
            get { return this.notes; }
            set { this.notes = value; OnPropertyChanged(); }
        }

        public string ItemId
        {
            get { return this.itemId; }
            set { this.itemId = value; OnPropertyChanged(); }
        }

        public Item Item
        {
            get { return this.item; }
            set { this.item = value; OnPropertyChanged(); }
        }

        internal void Remove(Notes notes)
        {
            this.NotesList.Remove(notes);
            this.IsEmpty = this.NotesList == null || this.NotesList.Count == 0;
        }

        internal void Add(Notes notes)
        {
            this.NotesList.Add(notes);
            this.IsEmpty = this.NotesList == null || this.NotesList.Count == 0;
        }

        public async void SaveData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    this.notes.ItemId = this.itemId;
                    this.notesDirectoryService.saveNotes(this.notes);
                    IsBusy  = false;
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Operação Realizada com sucesso!", "OK");
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

        public async void DeleteData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await this.notesDirectoryService.DeleteData(this.Notes);
                    IsBusy  = false;
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Operação Realizada com sucesso!", "OK");
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

        private async void CleanData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await this.notesDirectoryService.CleanData();
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

        public async void LoadDirectory()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    if (this.NotesList != null && this.NotesList.Any() && this.NotesList.Count > 0)
                    {
                        this.RemoveAll(this.NotesList);
                        await this.notesDirectoryService.CleanData();
                    }
                    Debug.WriteLine("Notes.LoadDirectory() id:"+ itemId);
                    var loadDirectory = await notesDirectoryService.GetNotes(item.Id);
                    Debug.WriteLine("Notes.LoadDirectory() count:" + loadDirectory.Count);
                    foreach (var note in loadDirectory)
                    {
                        this.NotesList.Add(note);
                    }
                    IsEmpty = this.NotesList == null || this.NotesList.Count == 0;
                    IsBusy = false;
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

        private void RemoveAll(ObservableCollection<Notes> notesList)
        {
            while (notesList.Count > 0)
            {
                notesList.RemoveAt(notesList.Count - 1);
            }
        }
    }
}
