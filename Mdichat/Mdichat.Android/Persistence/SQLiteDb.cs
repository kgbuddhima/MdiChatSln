﻿using System;
using System.IO;
using Mdichat.Droid.Persistence;
using SQLite;
using Mdichat.Persistence;
using SQLite;
using Xamarin.Forms;


[assembly: Dependency(typeof(SQLiteDb))]
namespace Mdichat.Droid.Persistence
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
