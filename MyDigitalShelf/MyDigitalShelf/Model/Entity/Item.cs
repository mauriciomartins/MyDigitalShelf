using Microsoft.WindowsAzure.MobileServices;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;
using Newtonsoft.Json;

namespace MyDigitalShelf.model
{
    [DataTable("item")]
    public class Item : ObservableBaseObject, IKeyObject
    {
        private string id;
        private string name;
        private string description;
        private string date;
        private int    evaluation;
        private string writer;
        private string publishingcompany;
        private string publishingDate;
        private string link;
        private string image;
        private string source;
        private int position;
        private string userId;

        [JsonProperty("position")]
        public int Position
        {
            get { return this.position; }
            set { this.position = value; OnPropertyChanged(); }
        }

        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(); }
        }

        [JsonProperty("user_id")]
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; OnPropertyChanged(); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(); }
        }

        [JsonProperty("publishingdate")]
        public string PublishingDate
        {
            get { return this.publishingDate; }
            set { this.publishingDate = value; OnPropertyChanged(); }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; OnPropertyChanged(); }
        }

        [JsonProperty("date")]
        public string Date
        {
            get { return this.date; }
            set { this.date = value; OnPropertyChanged(); }
        }

        [JsonProperty("evaluation")]
        public int Evaluation
        {
            get { return this.evaluation; }
            set { this.evaluation = value; OnPropertyChanged(); }
        }

        [JsonProperty("writer")]
        public string Writer
        {
            get { return this.writer; }
            set { this.writer = value; OnPropertyChanged(); }
        }

        [JsonProperty("publishingcompany")]
        public string Publishingcompany
        {
            get { return this.publishingcompany; }
            set { this.publishingcompany = value; OnPropertyChanged(); }
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

        [JsonProperty("source")]
        public string Source
        {
            get { return this.source; }
            set { this.source = value; OnPropertyChanged(); }
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
