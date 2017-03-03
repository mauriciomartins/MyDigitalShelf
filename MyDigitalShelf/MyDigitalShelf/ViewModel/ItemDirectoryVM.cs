using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.model
{
    class ItemDirectoryVM : ObservableBaseObject
    {
        private string searchName;
        private Item   item;
        private string userId;
        private ItemDirectoryService ItemDirectoryService;
        public ObservableCollection<Item> ItemList  { get; set; }
        private bool isBusy = false;


        public string SearchName
        {
            get { return this.searchName; }
            set { this.searchName = value; OnPropertyChanged(); }
        }

        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; OnPropertyChanged(); }
        }

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

        public bool ShowItemsSelectedMenu()
        {
            return item.Id != null && item.Id.Length>0;
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

        public Command SearchItemCommand
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
            this.SearchItemCommand        = new Command(() => SearchItem(), () => !this.IsBusy);
        }

        public async void SaveData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    this.item.UserId = this.userId;
                    this.ItemDirectoryService.saveItem(this.item);
                    IsBusy = false;
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Operação Realizada com sucesso!", "OK");
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

        public async void DeleteData()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await this.ItemDirectoryService.DeleteData(this.item);
                    IsBusy = false;
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso!", "Operação Realizada com sucesso!", "OK");
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
                        this.RemoveAll(this.ItemList);
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

        public async void SearchItem()
        {
            if (!IsBusy)
            {

                try
                {
                    IsBusy = true;
                    if (this.ItemList != null && this.ItemList.Any() && this.ItemList.Count > 0)
                    {
                        this.RemoveAll(this.ItemList);
                    }

                    if (this.SearchName==null||this.SearchName.Length == 0)
                    {
                        IsBusy = false;
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", "Nenhuma informação foi informada!", "OK");
                    }else {
                        var items = await this.ItemDirectoryService.GetBooks(SearchName.Replace(" ","+"));
                        if (items!=null)
                        {
                            Debug.WriteLine("SearchItem:" + items.Books.Length);
                            foreach (var book in items.Books)
                            {
                                if (book.VolumeInfo != null)
                                {
                                    Item item = new Item();
                                    item.Date = DateTime.Today.Year.ToString();
                                    item.Name = book.VolumeInfo.Name;
                                    item.Description = book.VolumeInfo.Description;
                                    item.Publishingcompany = book.VolumeInfo.Publishingcompany;
                                    item.PublishingDate = book.VolumeInfo.PublishingDate;
                                    if (book.VolumeInfo.Writers!=null)
                                    {
                                        item.Writer = string.Join(",", book.VolumeInfo.Writers.ToArray());
                                    }
                                    if (book.VolumeInfo.Image != null)
                                    {
                                        item.Image = book.VolumeInfo.Image.Thumbnail;
                                    }
                                
                                    item.Link = book.VolumeInfo.Link;
                                    item.Source = "Livro";
                                    this.ItemList.Add(item);
                                }
                            
                            }
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

        private void RemoveAll(ObservableCollection<Item> itemList)
        {
            while (itemList.Count > 0)
            {
                itemList.RemoveAt(itemList.Count - 1);
            }
        }

       
    }
}
