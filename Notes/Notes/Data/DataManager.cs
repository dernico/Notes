using Notes.Data.Local;
using Notes.Data.Remote;
using Notes.Models;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Notes.Data.DataManager))]
namespace Notes.Data
{
    public class DataManager : IDataManager
    {
        private INotesDatabase _local;
        private IRestService _remote;

        public DataManager()
        {
            _local = DependencyLoader.New<INotesDatabase>();
            _remote = DependencyLoader.New<IRestService>();
        }


        public UserModel getLocalUser()
        {
            var user = _local.getUser();
            return user;
        }

        public int storeLocalUser(UserModel user)
        {
            return _local.addUser(user);
        }

        public void clearLocalUser()
        {
            _local.clearUser();
        }

        public async Task<UserModel> loginRemote(string email, string password)
        {
            var user = await _remote.login(email, password);
            if (user != null)
            {
                return user;
            }
            return null;
        }


        public async Task<RestResponse<string>> registerRemote(string email, string password)
        {
            try
            {
                var resp = await _remote.register(email, password);
                return resp;
            }
            catch (Exception ex)
            {
                return new RestResponse<string> {
                    Success = false,
                    Message = ex.ToString(),
                    ResponseObject = null
                };
            }
        }

        public async Task<List<NoteModel>> loadUserNotes(UserModel user)
        {
            try
            {
                var users = await _remote.loadUserNotes(user);
                return users;
            }
            catch (Exception ex)
            {
                return new List<NoteModel>() {
                    new NoteModel
                    {
                        title = "Error occured while loading ...",
                        content = "try loggout and login again"
                    }
                };
            }
        }

        public async Task<NoteModel> saveNewNoteRemote(UserModel user, NoteModel note)
        {
            try
            {
                note = await _remote.saveNewNote(user, note);
                return note;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<NoteModel> updateNoteRemote(UserModel user, NoteModel note)
        {
            try
            {
                note = await _remote.updateNote(user, note);
                return note;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> deleteNoteRemote(UserModel user, NoteModel note)
        {
            try
            {
                return await _remote.deleteNote(user, note);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
