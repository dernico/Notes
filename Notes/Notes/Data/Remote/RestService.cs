using Notes.Data.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Notes.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Notes.Data.Converter;
using System.IO;
using Newtonsoft.Json.Linq;

[assembly: Dependency(typeof(RestService))]
namespace Notes.Data.Remote
{
    public class RestService : IRestService
    {
        private HttpClient _http;

        public RestService()
        {
            _http = new HttpClient();
        }

        public async Task<bool> deleteNote(UserModel user, NoteModel note)
        {
            var url = Constants.DeleteNoteAddress + "?token=" + user.token;
            url += "&noteid=" + note._id;

            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);
                if(!json["error"].HasValues)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<RestResponse<NoteModel>> updateNote(UserModel user, NoteModel note)
        {
            var url = Constants.UpdateNoteAddress + "?token=" + user.token;

            var json = JsonConvert.SerializeObject(note);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var restresponse = new RestResponse<NoteModel>();
            var response = await _http.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                restresponse.ResponseObject = JsonConvert.DeserializeObject<NoteModel>(content);
                restresponse.Success = true;
                return restresponse;
            }
            restresponse.Success = false;
            restresponse.Message = response.ReasonPhrase;
            return restresponse;
        }

        public async Task<RestResponse<NoteModel>> saveNewNote(UserModel user, NoteModel note)
        {
            var url = Constants.AddNoteAddress + "?token=" + user.token;
            url += "&title=" + WebUtility.UrlEncode(note.title);
            url += "&content=" + WebUtility.UrlEncode(note.content);
            url += "&startDate=" + WebUtility.UrlDecode(note.startDate.ToString());
            url += "&endDate=" + WebUtility.UrlDecode(note.endDate.ToString());

            var restresponse = new RestResponse<NoteModel>();
            var result = await _http.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                restresponse.ResponseObject = JsonConvert.DeserializeObject<NoteModel>(content);
                restresponse.Success = true;
                return restresponse;
            }
            restresponse.Success = false;
            restresponse.Message = result.ReasonPhrase;
            return restresponse;
        }

        public async Task<List<NoteModel>> loadUserNotes(UserModel user)
        {
            var url = Constants.NotesAddress + "?token=" + user.token;

            var result = await _http.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync();
                using (var sr = new StreamReader(content))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Converters.Add(new DateTimeConverter());
                        var noteResult = serializer.Deserialize<NotesResult>(reader);
                        return noteResult.notes;
                    }
                }
            }
            return null;
        }

        public async Task<RestResponse<UserModel>> login(string email, string password)
        {

            var json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var restresponse = new RestResponse<UserModel>();

            var result = await _http.PostAsync(Constants.LoginAddress, httpContent);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserModel>(content);
                restresponse.Success = true;
                restresponse.ResponseObject = user;
                return restresponse;
            }
            restresponse.Success = false;
            restresponse.Message = result.ReasonPhrase;
            return restresponse;
        }

        public async Task<RestResponse<string>> register(string email, string password)
        {

            var json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _http.PostAsync(Constants.RegisterAddress, httpContent);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var jresult = JObject.Parse(content);
                return new RestResponse<string> {
                    Success = jresult["success"].Value<bool>(),
                    Message = jresult["response"].Value<string>(),
                    ResponseObject = content
                };
            }
            return new RestResponse<string>
            {
                Success = false,
                Message = "Requeset was not successfull: " + result.StatusCode,
                ResponseObject = null
            }; ;
        }


    }
}
