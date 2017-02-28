using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.model
{
    class CategoryDirectory : ObservableBaseObject
    {
        private ObservableCollection<Category> categoryList = new ObservableCollection<Category>();
        public ObservableCollection<Category> CategoryList
        {
            get { return this.categoryList; }
            set { this.categoryList = value; OnPropertyChanged(); }
        }
    }
}
