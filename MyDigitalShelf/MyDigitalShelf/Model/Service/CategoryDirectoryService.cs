using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyDigitalShelf.Model.Service
{
    class CategoryDirectoryService
    {
        IMobileServiceClient Client;
        IMobileServiceSyncTable<Category> Table;
        const string dbPath = "MyDigitalShelfDB";
        const string MyAppServiceURL = "http://mydigitalshelf.azurewebsites.net";

        public CategoryDirectoryService()
        {
            Client = new MobileServiceClient(MyAppServiceURL);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Category>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<Category>();
        }

        public async Task<List<Category>> GetCategories()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await this.SyncAsync();
            }
            var Items = await this.GetTable("9d573dfa-bb3f-4eae-ab63-95c126401fd2");
            return Items.ToList();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> errors = null;
            try
            {
                await Client.SyncContext.PushAsync();
                await Table.PullAsync("GetAllCategories", Table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushError)
            {
                if (pushError.PushResult != null)
                {
                    errors = pushError.PushResult.Errors;
                }
            }
        }

        public Task<IEnumerable<Category>> GetTable(string id)
        {
            return Table.Where(c => c.Id == id).ToEnumerableAsync();
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync("GetAllCategories", Table.CreateQuery(), new System.Threading.CancellationToken());
        }

        public async void saveCategory(Category category)
        {
            Random rdn = new Random(DateTime.Now.Millisecond);
            string key = rdn.Next(12384748, 32384748).ToString();
            category.Key = key;
            await this.AddCategory(category);
        }


        public async Task AddCategory(Category category)
        {
            await Table.InsertAsync(category);
            await SyncAsync();
        }
    }
}
