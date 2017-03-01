using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MyDigitalShelf.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalShelf.Model.Service
{
    class NotesDirectoryService
    {

        IMobileServiceClient Client;
        IMobileServiceSyncTable<Notes> Table;
        const string dbPath = "MyDigitalShelfDB";
        const string MyAppServiceURL = "http://mydigitalshelf.azurewebsites.net";

        public NotesDirectoryService()
        {
            Client = new MobileServiceClient(MyAppServiceURL);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Notes>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<Notes>();
        }

        public async Task<List<Notes>> GetNotes(string userId)
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await this.SyncAsync();
            }
            var Notess = await this.GetTable(userId);    
            return Notess.ToList();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> errors = null;
            try
            {
                await Client.SyncContext.PushAsync();
                await Table.PullAsync("GetAllNotes", Table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushError)
            {
                if (pushError.PushResult != null)
                {
                    errors = pushError.PushResult.Errors;
                }
            }
        }

        public Task<IEnumerable<Notes>> GetTable(string user_id)
        {
            return Table.Where(c=>c.ItemId==user_id).ToEnumerableAsync();
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync("GetAllNotes", Table.CreateQuery(), new System.Threading.CancellationToken());
        }

        public async void saveNotes(Notes Notes)
        {
            Random rdn = new Random(DateTime.Now.Millisecond);
            string key = rdn.Next(12384748, 32384748).ToString();
            Notes.Key = key;
            await this.AddNotes(Notes);
        }


        public async Task AddNotes(Notes Notes)
        {
            await Table.InsertAsync(Notes);

            await SyncAsync();
        }

    }
}
