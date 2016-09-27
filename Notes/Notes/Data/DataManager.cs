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

        public async Task<RestResponse<UserModel>> loginRemote(string email, string password)
        {
            var result = new RestResponse<UserModel>();
            try
            {
                result = await _remote.login(email, password);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Could not connect to the service. Try again later :(";
            }
            return result;
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

        public async Task<RestResponse<NoteModel>> saveNewNoteRemote(UserModel user, NoteModel note)
        {
            var response = new RestResponse<NoteModel>();
            try
            {
                response = await _remote.saveNewNote(user, note);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Connection error. Try again later :(";
            }
            return response;
        }

        public async Task<RestResponse<NoteModel>> updateNoteRemote(UserModel user, NoteModel note)
        {
            var response = new RestResponse<NoteModel>();
            try
            {
                response = await _remote.updateNote(user, note);
                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Connection error. Try again later :(";
            }
            return response;
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

        public async Task<RestResponse<List<AutoCompleteOption>>> placesAutoComplete(UserModel user, string input)
        {
            RestResponse<List<AutoCompleteOption>> response = new RestResponse<List<AutoCompleteOption>>();
            try
            {
                response = await _remote.placesAutoComplete(user, input);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<RestResponse<PlaceModel>> placesDetails(UserModel user, string placeid)
        {
            var response = new RestResponse<PlaceModel>();

            try
            {
                response = await _remote.placesDetails(user, placeid);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }
    }
}
