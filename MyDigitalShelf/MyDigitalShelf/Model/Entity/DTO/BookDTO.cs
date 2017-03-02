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
  
    public class BookDTO 
    {
        private string kind;
        private string id;
        private VolumeInfoDTO volumeInfo;

        [JsonProperty("Kind")]
        public string Kind
        {
            get { return this.kind; }
            set { this.kind = value; }
        }

        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.id = value;}
        }

        [JsonProperty("volumeInfo")]
        public VolumeInfoDTO VolumeInfo
        {
            get { return this.volumeInfo; }
            set { this.volumeInfo = value;}
        }
        
    }
}
