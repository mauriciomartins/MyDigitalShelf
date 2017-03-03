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
    public partial class NotesDetailPage : ContentPage
    {
        private bool isNew;
        private model.NotesDirectoryVM notesDirectoryVM = new NotesDirectoryVM();
        public NotesDetailPage(string itemId,Notes notes)
        {
            InitializeComponent();
            this.notesDirectoryVM.Notes  = notes;
            this.isNew = (notes.Id == null || notes.Id.Length == 0) ;
            this.notesDirectoryVM.ItemId = itemId;
            this.BindingContext = notesDirectoryVM;
            this.SaveButton.Clicked   += SaveButton_Clicked;
            this.DeleteButton.Clicked += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (notesDirectoryVM.Notes.Id == null || notesDirectoryVM.Notes.Id.Length == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nota não cadastrada!", "OK");
            }
            else
            {
                notesDirectoryVM.DeleteData();
                Page page = await Navigation.PopAsync();
                NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                ((NotesPage)navPage.CurrentPage).RemoveNote(this.notesDirectoryVM.Notes);
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (this.NotesDescription.Text == null || this.NotesDescription.Text.Length == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nenhuma descrição foi informada", "OK");
            }
            else
            {
                notesDirectoryVM.SaveData();
                Page page = await Navigation.PopAsync();
                if (this.isNew)
                {
                    NavigationPage navPage = (NavigationPage)App.Current.MainPage;
                    ((NotesPage)navPage.CurrentPage).AppenNote(this.notesDirectoryVM.Notes);
                }
            }
        }
    }
}
