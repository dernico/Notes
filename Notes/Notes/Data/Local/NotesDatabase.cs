using Notes.Models;
using Notes.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Notes.Data.Local.NotesDatabase))]
namespace Notes.Data.Local
{
    public class NotesDatabase : INotesDatabase
    {
        private SQLiteConnection _con;

        public NotesDatabase()
        {
            _con = DependencyLoader.New<ISQLite>().getConnection();
            _con.CreateTable<NoteModel>();
            _con.CreateTable<UserModel>();
        }

        public UserModel getUser()
        {
            var user = _con.Query<UserModel>("select * from UserModel").FirstOrDefault();
            return user;
        }

        public int addUser(UserModel newUser)
        {
            clearUser();
            int code = _con.Insert(newUser);
            return code;
        }

        public void clearUser() {
            _con.DeleteAll<UserModel>();
        }

    }
}
