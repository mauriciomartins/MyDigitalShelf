using Microsoft.WindowsAzure.MobileServices;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;
using Newtonsoft.Json;
using System;

namespace MyDigitalShelf.MyDigitalShelf
{
    [DataTable("Category")]
    public class Category: ObservableBaseObject,IKeyObject
    {
        private string value;
        private string description;
        private string image;

        [JsonProperty("id")]
        public string Id
        {
            get;set;
        }

        [JsonProperty("image")]
        public string Image
        {
            get { return this.image; }
            set { this.image = value; OnPropertyChanged(); }
        }
        [JsonProperty("value")]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; OnPropertyChanged(); }
        }
        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; OnPropertyChanged(); }
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
