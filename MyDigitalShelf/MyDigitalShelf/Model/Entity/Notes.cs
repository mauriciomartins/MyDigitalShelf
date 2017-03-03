using Microsoft.WindowsAzure.MobileServices;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;
using Newtonsoft.Json;

namespace MyDigitalShelf.model
{
    [DataTable("notes")]
    public class Notes : ObservableBaseObject, IKeyObject
    {
        private string description;
        private string link;
        private string image;
        private string itemId;
        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        [JsonProperty("item_id")]
        public string ItemId
        {
            get { return this.itemId; }
            set { this.itemId = value; OnPropertyChanged(); }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; OnPropertyChanged(); }
        }

        [JsonProperty("link")]
        public string Link
        {
            get { return this.link; }
            set { this.link = value; OnPropertyChanged(); }
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
