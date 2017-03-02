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
    public class SearchingInfoDTO : ObservableBaseObject, IKeyObject
    {
        private string kind;
        private string totalItems;
        private List<BookDTO> books; 

        [JsonProperty("Kind")]
        public string Kind
        {
            get { return this.kind; }
            set { this.kind = value; OnPropertyChanged(); }
        }

        [JsonProperty("totalItems")]
        public string TotalItems
        {
            get { return this.totalItems; }
            set { this.totalItems = value; OnPropertyChanged(); }
        }

        [JsonProperty("items")]
        public List<BookDTO> Books
        {
            get { return this.books; }
            set { this.books = value; OnPropertyChanged(); }
        }

        public string Key
        {
            get;
            set;
        }      
    }
}
