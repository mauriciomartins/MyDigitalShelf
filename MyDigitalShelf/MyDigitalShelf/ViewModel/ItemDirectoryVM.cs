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
    class ItemDirectoryVM : ObservableBaseObject
    {
        private ItemDirectoryService ItemDirectoryService;
        public ObservableCollection<Item> ItemList  { get; set; }
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

        public Command LoadItemDirectoryCommand
        {
            get;set;
        }

        public ItemDirectoryVM()
        {
            this.ItemDirectoryService = new ItemDirectoryService();
            this.ItemList = new ObservableCollection<Item>();
            this.IsBusy = false;
            this.LoadItemDirectoryCommand = new Command(()=> LoadDirectory(),()=>!this.IsBusy);
            this.CleanDataCommand             = new Command(() => CleanData(), () => !this.IsBusy);
        }

        private async void CleanData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await this.ItemDirectoryService.CleanData();
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
                
                try
                {
                    IsBusy = true;
                    this.ItemList.Clear();
                    var items =  await this.ItemDirectoryService.GetItems();
                    foreach (var Item in items)
                    {
                        this.ItemList.Add(Item);
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
