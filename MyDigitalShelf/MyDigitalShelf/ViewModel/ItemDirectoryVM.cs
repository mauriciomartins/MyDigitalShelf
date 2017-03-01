using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections;
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
        private Item item;
        private ItemDirectoryService ItemDirectoryService;
        public ObservableCollection<Item> ItemList  { get; set; }
        private bool isBusy = false;


        public Item Item
        {
            get { return this.item; }
            set { this.item = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public Command SaveCommand
        {
            get; set;
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
            this.CleanDataCommand         = new Command(() => CleanData(), () => !this.IsBusy);
            this.SaveCommand              = new Command(() => SaveData(), () => !this.IsBusy);
        }

        private async void SaveData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    this.item.UserId = "df258d04-d3da-4380-a528-113d34d9e26c";
                    this.ItemDirectoryService.saveItem(this.item);
                    this.item = new Item();
                    IsBusy = false;
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Cadastro realizado com sucesso!", "OK");
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
                    if(this.ItemList!=null&& this.ItemList.Any()&& this.ItemList.Count>0)
                    {
                        this.RemoveAll();
                        await this.ItemDirectoryService.CleanData();
                    }
                    
                    var items =  await this.ItemDirectoryService.GetItems();
                    if (items.Any()) { 
                        foreach (var Item in items)
                        {
                            this.ItemList.Add(Item);
                        }
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

        public  void RemoveAll()
        {
            while (this.ItemList.Count > 0)
            {
                ItemList.RemoveAt(this.ItemList.Count - 1);
            }
        }
    }
}
