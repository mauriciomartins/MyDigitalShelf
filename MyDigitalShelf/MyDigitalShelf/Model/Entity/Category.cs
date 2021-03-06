﻿using Microsoft.WindowsAzure.MobileServices;
using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;
using Newtonsoft.Json;
using System;

namespace MyDigitalShelf.MyDigitalShelf
{
    [DataTable("Category")]
    public class Category: ObservableBaseObject,IKeyObject
    {
        private string name;
        private string description;
       
        [JsonProperty("id")]
        public string Id
        {
            get;set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(); }
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
