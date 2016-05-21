using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Remote
{
    public interface IRestService
    {
        Task<UserModel> login(string email, string password);
        Task<RestResponse<string>> register(string email, string password);
        Task<List<NoteModel>> loadUserNotes(UserModel user);
        Task<NoteModel> saveNewNote(UserModel user, NoteModel note);
        Task<NoteModel> updateNote(UserModel user, NoteModel note);
        Task<bool> deleteNote(UserModel user, NoteModel note);
    }
}
