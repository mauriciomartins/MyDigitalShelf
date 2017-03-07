using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.model
{
    public class ProfileVM : ObservableBaseObject
    {
        private User   user;
        private UserDirectoryService userDirectoryService;
        private bool isBusy  = false;
        private bool isMine  = true;
        
        public User User
        {
            get { return this.user; }
            set { this.user = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }
        
        public bool IsMine
        {
            get { return this.isMine; }
            set { this.isMine = value; OnPropertyChanged(); }
        }
       
        public Command SaveCommand
        {
            get; set;
        }

      
        public ProfileVM()
        {
            this.userDirectoryService = new UserDirectoryService();
            this.IsBusy                   = false;
            this.SaveCommand              = new Command(() => SaveData(), () => !this.IsBusy);
        }

        public async void SaveData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    this.userDirectoryService.saveUser(this.User);
                    IsBusy = false;
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
    }
}
