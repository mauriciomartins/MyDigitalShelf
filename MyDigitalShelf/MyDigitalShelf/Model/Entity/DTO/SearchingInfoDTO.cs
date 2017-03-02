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
    public class SearchingInfoDTO
    {
        private string kind;
        private string totalItems;
        private BookDTO[] books; 

        [JsonProperty("Kind")]
        public string Kind
        {
            get { return this.kind; }
            set { this.kind = value; }
        }

        [JsonProperty("totalItems")]
        public string TotalItems
        {
            get { return this.totalItems; }
            set { this.totalItems = value; }
        }

        [JsonProperty("items")]
        public BookDTO[] Books
        {
            get { return this.books; }
            set { this.books = value; }
        }  
    }
}
