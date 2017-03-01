﻿using MyDigitalShelf.model;
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
        IMobileServiceSyncTable<Category> Table;
        const string dbPath = "MyDigitalShelfDB";
        const string MyAppServiceURL = "http://mydigitalshelf.azurewebsites.net";
        public AzureService()
        {
            Client = new MobileServiceClient(MyAppServiceURL);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<T>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<Category>();
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


        public Task<IEnumerable<Category>> GetTable()
        {
            //return Table.ToEnumerableAsync();
            return Table.Where(c => c.Id=="9d573dfa-bb3f-4eae-ab63-95c126401fd2").ToEnumerableAsync();
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync("GetAllCategories", Table.CreateQuery(), new System.Threading.CancellationToken());
        }
    }
}
