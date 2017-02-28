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
    class CategoryDirectoryVM : ObservableBaseObject
    {
        public ObservableCollection<Category> CategoryList  { get; set; }
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public Command CleanDataCommand
        {
            get; set;
        }

        public Command LoadCategoryDirectoryCommand
        {
            get;set;
        }

        public CategoryDirectoryVM()
        {
            this.CategoryList = new ObservableCollection<Category>();
            this.IsBusy = false;
            this.LoadCategoryDirectoryCommand = new Command(()=> LoadDirectory(),()=>!this.IsBusy);
            this.CleanDataCommand             = new Command(() => CleanData(), () => !this.IsBusy);
        }

        private async void CleanData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await CategoryDirectoryService.CleanData();
                }
                catch (Exception e)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", e.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

        }

        public async void LoadDirectory()
        {
            if (!IsBusy)
            {
                

                /*
                await Task.Delay(3000);
                var loadDirectory = CategoryDirectoryService.LoadCategoryDirectory();
                
                foreach (var category in loadDirectory.CategoryList)
                {
                    this.CategoryList.Add(category);
                }
                */
                try
                {
                    IsBusy = true;
                    this.CategoryList.Clear();
                    var categories =  await CategoryDirectoryService.GetCategories();
                    foreach (var category in categories)
                    {
                        this.CategoryList.Add(category);
                    }
                }
                catch (Exception e)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", e.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
