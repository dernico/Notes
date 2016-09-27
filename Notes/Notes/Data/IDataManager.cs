using Notes.Data.Remote;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data
{
    public interface IDataManager
    {
        UserModel getLocalUser();
        int storeLocalUser(UserModel user);
        void clearLocalUser();
        Task<RestResponse<UserModel>> loginRemote(string email, string password);
        Task<RestResponse<string>> registerRemote(string email, string password);
        Task<List<NoteModel>> loadUserNotes(UserModel user);
        Task<RestResponse<NoteModel>> saveNewNoteRemote(UserModel user, NoteModel note);
        Task<RestResponse<NoteModel>> updateNoteRemote(UserModel user, NoteModel note);
        Task<bool> deleteNoteRemote(UserModel user, NoteModel note);
        Task<RestResponse<List<AutoCompleteOption>>> placesAutoComplete(UserModel user, string input);
        Task<RestResponse<PlaceModel>> placesDetails(UserModel user, string placeid);
    }
}
