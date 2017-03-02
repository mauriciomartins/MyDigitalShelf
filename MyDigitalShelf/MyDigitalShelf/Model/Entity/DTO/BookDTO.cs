using Microsoft.WindowsAzure.MobileServices;
using MyDigitalShelf.Model.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.Model.Entity
{
    [DataTable("item")]
    public class BookDTO : ObservableBaseObject, IKeyObject
    {
        private string kind;
        private string id;
        private VolumeInfoDTO volumeInfo;

        [JsonProperty("Kind")]
        public string Kind
        {
            get { return this.kind; }
            set { this.kind = value; OnPropertyChanged(); }
        }

        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(); }
        }

        [JsonProperty("volumeInfo")]
        public VolumeInfoDTO VolumeInfo
        {
            get { return this.volumeInfo; }
            set { this.volumeInfo = value; OnPropertyChanged(); }
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
