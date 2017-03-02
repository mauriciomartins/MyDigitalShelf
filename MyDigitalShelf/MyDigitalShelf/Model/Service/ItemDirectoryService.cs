using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MyDigitalShelf.model;
using MyDigitalShelf.Model.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.Model.Service
{
    class ItemDirectoryService
    {

        IMobileServiceClient Client;
        IMobileServiceSyncTable<Item> Table;
        const string dbPath = "MyDigitalShelfDB";
        const string MyAppServiceURL = "http://mydigitalshelf.azurewebsites.net";

        public ItemDirectoryService()
        {
            Client = new MobileServiceClient(MyAppServiceURL);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Item>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<Item>();
        }

        public async Task<List<Item>> GetItems()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await this.SyncAsync();
            }
            var Items = await this.GetTable("df258d04-d3da-4380-a528-113d34d9e26c");    
            return Items.ToList();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> errors = null;
            try
            {
                await Client.SyncContext.PushAsync();
                await Table.PullAsync("GetAllItems", Table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushError)
            {
                if (pushError.PushResult != null)
                {
                    errors = pushError.PushResult.Errors;
                }
            }
        }

        public Task<IEnumerable<Item>> GetTable(string user_id)
        {
            return Table.Where(c=>c.UserId==user_id).ToEnumerableAsync();
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync("GetAllItems", Table.CreateQuery(), new System.Threading.CancellationToken());
        }

        public async void saveItem(Item Item)
        {
            Random rdn = new Random(DateTime.Now.Millisecond);
            string key = rdn.Next(12384748, 32384748).ToString();
            Item.Key = key;
            await this.AddItem(Item);
        }


        public async Task AddItem(Item Item)
        {
            if(Item.Id==null|| Item.Id == "")
            {
                await Table.InsertAsync(Item);
            }
            else
            {
                await Table.UpdateAsync(Item);
            }
            

            await SyncAsync();
        }


        public async Task<SearchingInfoDTO> GetBooks(string searchName)
        {
            SearchingInfoDTO searchInfo;
            var URLWebAPI = "https://www.googleapis.com/books/v1/volumes?q="+searchName;
            Debug.WriteLine("GetBooks?" + URLWebAPI);
            using (var Client = new System.Net.Http.HttpClient())
            {
                var JSON = await Client.GetStringAsync(URLWebAPI);
                searchInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchingInfoDTO>(JSON);
            }
            return searchInfo;
        }
    }
}
