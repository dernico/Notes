﻿using Notes.Data.Local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Notes.UWP.SQLite_UWP))]
namespace Notes.UWP
{
    public class SQLite_UWP : ISQLite
    {
        public SQLite_UWP() { }

        public SQLite.SQLiteConnection getConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
