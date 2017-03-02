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
    
    public class VolumeInfoDTO 
    {
        private string id;
        private string name;
        private string description;
        private string date;
        private int evaluation;
        private List<string> writers;
        private string publishingcompany;
        private string publishingDate;
        private string source;
        private int position;
        private string userId;
        private BookImageDTO image;
        private string link;

        [JsonProperty("position")]
        public int Position
        {
            get { return this.position; }
            set { this.position = value;  }
        }

        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.id = value;  }
        }

        [JsonProperty("user_id")]
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value;  }
        }

        [JsonProperty("title")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value;  }
        }

        [JsonProperty("publishedDate")]
        public string PublishingDate
        {
            get { return this.publishingDate; }
            set { this.publishingDate = value;  }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value;  }
        }

        [JsonProperty("date")]
        public string Date
        {
            get { return this.date; }
            set { this.date = value;  }
        }

        [JsonProperty("evaluation")]
        public int Evaluation
        {
            get { return this.evaluation; }
            set { this.evaluation = value;  }
        }

        [JsonProperty("authors")]
        public List<string> Writers
        {
            get { return this.writers; }
            set { this.writers = value;  }
        }

        [JsonProperty("publisher")]
        public string Publishingcompany
        {
            get { return this.publishingcompany; }
            set { this.publishingcompany = value;  }
        }

        [JsonProperty("imageLinks")]
        public BookImageDTO Image
        {
            get { return this.image; }
            set { this.image = value;  }
        }

        [JsonProperty("previewLink")]
        public string Link
        {
            get { return this.link; }
            set { this.link = value;  }
        }
   
    }
}
