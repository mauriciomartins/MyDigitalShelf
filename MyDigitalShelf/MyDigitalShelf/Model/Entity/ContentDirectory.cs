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
        private ObservableCollection<Content> contentList = new ObservableCollection<Content>();
        public ObservableCollection<Content> ContentList
        {
            get { return this.contentList; }
            set { this.contentList = value; OnPropertyChanged(); }
        }
    }
}
