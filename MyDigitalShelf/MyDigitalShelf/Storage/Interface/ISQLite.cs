using SQLite;

namespace MyDigitalShelf.Model.Storage
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}