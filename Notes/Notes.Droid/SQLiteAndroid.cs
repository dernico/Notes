using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Notes.Data.Local;
using SQLite;
using System.IO;

[assembly: Dependency(typeof(Notes.Droid.SQLiteAndroid))]
namespace Notes.Droid
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteConnection getConnection()
        {
            var sqliteFilename = "NotesDB.db3";
            string documentsPath 
                = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}