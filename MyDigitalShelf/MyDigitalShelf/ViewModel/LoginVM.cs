using MyDigitalShelf.model;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.ViewModel
{
    class LoginVM : ObservableBaseObject
    {
        private bool isBusy = false;
        private UserDirectoryService userDirectoryService;
        private string email;
        private string password;
        public LoginVM()
        {
            this.userDirectoryService = new UserDirectoryService();
            this.IsBusy = false;
            this.LoginCommand = new Command(() => LoginDirectory(), () => !this.IsBusy);
            
        }

        public Command LoginCommand
        {
            get; set;
        }

        public async Task<User> LoginDirectory()
        {
            User user = null;

            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    if (this.email == null || this.email.Length == 0|| this.password == null || this.password.Length == 0)
                    {
                        IsBusy = false;
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "O Email e a senha devem ser informados!", "OK");
                    }
                    else
                    {
                         user = await this.userDirectoryService.LoginUser(this.email, this.password);
                        if (user == null)
                        {
                            IsBusy = false;
                            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Email ou senha inválida!", "OK");
                        }
                    }
                }
                catch (Exception e)
                {
                    IsBusy = false;
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", e.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
                        
            return user;
         }
        
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; OnPropertyChanged(); }
        }
    }
}
