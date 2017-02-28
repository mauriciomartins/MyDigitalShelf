using MyDigitalShelf.model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDigitalShelf.MyDigitalShelf;

namespace MyDigitalShelf.Model.Service
{
    public class AzureService<T>
    {
        IMobileServiceClient Client;
        IMobileServiceSyncTable<T> Table;
        const string dbPath = "MyDigitalShelfDB";
        const string MyAppServiceURL = "http://AlphabetStepByStep.azurewebsites.net";
        public AzureService()
        {
            Client = new MobileServiceClient(MyAppServiceURL);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Category>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<T>();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> errors = null;
            try
            {
                await Client.SyncContext.PushAsync();
                await Table.PullAsync("GetAllCategories",Table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushError)
            {
                if (pushError.PushResult != null)
                {
                    errors = pushError.PushResult.Errors;
                }
            }
        }


        public Task<IEnumerable<T>> GetTable()
        {
            return Table.ToEnumerableAsync();
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync("GetAllCategories", Table.CreateQuery(), new System.Threading.CancellationToken());
        }
    }
}
