using SQLite;
using MyDigitalShelf.Droid.Storage.Implementation;
using Xamarin.Forms;
using System.IO;
using MyDigitalShelf.Model.Storage;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace MyDigitalShelf.Droid.Storage.Implementation
{
    public class SQLiteDroid : ISQLite
    {
        public SQLiteDroid()
        {

        }
        public SQLiteConnection GetConnection()
        {
            SQLitePCL.Batteries.Init();
            var sqliteFileName = "MyDigitalShelf.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, sqliteFileName);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}