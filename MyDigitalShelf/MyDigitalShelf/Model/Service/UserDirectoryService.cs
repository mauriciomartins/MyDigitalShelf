using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MyDigitalShelf.model;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyDigitalShelf.Model.Service
{
    class UserDirectoryService
    {
        IMobileServiceClient Client;
        IMobileServiceSyncTable<User> Table;
        const string dbPath = "MyDigitalShelfDB";
        const string MyAppServiceURL = "http://mydigitalshelf.azurewebsites.net";

        public UserDirectoryService()
        {
            Client = new MobileServiceClient(MyAppServiceURL);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<User>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<User>();
        }

        public async Task<List<User>> GetUsers()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await this.SyncAsync();
            }
            var Items = await this.GetTable("9d573dfa-bb3f-4eae-ab63-95c126401fd2");
            return Items.ToList();
        }

        public async Task<List<User>> Login(string email, string password)
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await this.SyncAsync();
            }
            var Items = await this.GetUser(email, password);
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

        public Task<IEnumerable<User>> GetTable(string id)
        {
            return Table.Where(c => c.Id == id).ToEnumerableAsync();
        }

        public Task<IEnumerable<User>> GetUser(string email,string password)
        {
            return Table.Where(c => c.Email == email).Where(c => c.Password == password).ToEnumerableAsync();
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync("GetAllCategories", Table.CreateQuery(), new System.Threading.CancellationToken());
        }

        public async void saveUser(User User)
        {
            Random rdn = new Random(DateTime.Now.Millisecond);
            string key = rdn.Next(12384748, 32384748).ToString();
            User.Key = key;
            await this.AddUser(User);
        }


        public async Task AddUser(User User)
        {
            await Table.InsertAsync(User);

            await SyncAsync();
        }
    }
}
