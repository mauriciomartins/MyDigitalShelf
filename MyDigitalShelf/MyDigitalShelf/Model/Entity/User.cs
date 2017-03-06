using Microsoft.WindowsAzure.MobileServices;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.model
{
    [DataTable("user")]
    public class User : ObservableBaseObject, IKeyObject
    {
        private string name;
        private string email;
        private string password;
        private string birthDate;
        private string image;

        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(); }
        }
        [JsonProperty("email")]
        public string Email
        {
            get { return this.email; }
            set { this.email = value; OnPropertyChanged(); }
        }

        [JsonProperty("password")]
        public string Password
        {
            get { return this.password; }
            set { this.password = value; OnPropertyChanged(); }
        }

        [JsonProperty("birthdate")]
        public string BirthDate
        {
            get { return this.birthDate; }
            set { this.birthDate = value; OnPropertyChanged(); }
        }

        [JsonProperty("image")]
        public string Image
        {
            get { return this.image; }
            set { this.image = value; OnPropertyChanged(); }
        }

        public string Key
        {
            get;
            set;
        }

        [Version]
        public string AzureVersion { get; set; }

    }
}
