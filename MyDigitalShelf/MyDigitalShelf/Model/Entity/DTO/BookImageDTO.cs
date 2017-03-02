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
   
    public class BookImageDTO 
    {
        private string thumbnail;

        [JsonProperty("thumbnail")]
        public string Thumbnail
        {
            get { return this.thumbnail; }
            set { this.thumbnail = value; }
        }
    }
}
