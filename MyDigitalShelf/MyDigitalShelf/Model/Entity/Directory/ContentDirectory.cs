using MyDigitalShelf.Model.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.model
{
    class ContentDirectory : ObservableBaseObject
    {
        private ObservableCollection<Item> contentList = new ObservableCollection<Item>();
        public ObservableCollection<Item> ContentList
        {
            get { return this.contentList; }
            set { this.contentList = value; OnPropertyChanged(); }
        }
    }
}
