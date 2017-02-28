using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.model
{
    class CategoryDetailDirectoryVM : ObservableBaseObject
    {
        private Category categoryItem;
        public ObservableCollection<Category> CategoryList  { get; set; }
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public Category CategoryItem
        {
            get { return this.categoryItem; }
            set { this.categoryItem = value; OnPropertyChanged(); }
        }

        public Command SaveCategoryDirectoryCommand
        {
            get;set;
        }

        public CategoryDetailDirectoryVM()
        {
            this.CategoryList = new ObservableCollection<Category>();
            this.IsBusy = false;
            this.SaveCategoryDirectoryCommand = new Command(()=> SaveDirectory(),()=>!this.IsBusy);
            this.categoryItem = new Category();
            this.categoryItem.Value       = "TEste1";
            this.categoryItem.Description = "TEste2";
        }

        public  void SaveDirectory()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                Task.Delay(3000);

                CategoryDirectoryService.saveCategory(categoryItem);
               
                IsBusy = false;
            }
        }
    }
}
