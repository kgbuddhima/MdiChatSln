using System;
using System.IO;
using MdiChat.Droid.Persistence;
using SQLite;
using MdiChat.Persistence;
using SQLite;
using Xamarin.Forms;


[assembly: Dependency(typeof(SQLiteDb))]
namespace MdiChat.Droid.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MDI.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}
