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
    [DataTable("imageLinks")]
    public class BookImageDTO : ObservableBaseObject, IKeyObject
    {
        private string thumbnail;
        [JsonProperty("smallThumbnail")]
        public string Thumbnail
        {
            get { return this.thumbnail; }
            set { this.thumbnail = value; OnPropertyChanged(); }
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
