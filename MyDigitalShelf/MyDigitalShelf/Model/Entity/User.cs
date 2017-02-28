using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.model
{
    class User : ObservableBaseObject, IKeyObject
    {
        private string name;
        private string email;
        private string password;
        private string birthDate;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
        public string BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; OnPropertyChanged(); }
        }

        public string Key
        {
            get;
            set;
        }
    }
}
