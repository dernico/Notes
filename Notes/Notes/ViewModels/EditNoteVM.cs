using Notes.Data;
using Notes.Data.Remote;
using Notes.Models;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notes.ViewModels
{
    public class EditNoteVM : BaseViewModel
    {
        private IDataManager _data;
        private INavigationService _nav;
        private NoteModel _note;

        private bool _isEdit;
        public bool IsEdit {
            get
            {
                return _isEdit;
            }
            set
            {
                _isEdit = value;
                Changed("IsEdit");
            }
        }
        
        public string Title
        {
            get
            {
                return _note.title;
            }

            set
            {
                _note.title = value;
                Changed("Title");
            }
        }
        
        public string Content
        {
            get
            {
                return _note.content;
            }

            set
            {
                _note.content = value;
                Changed("Content");
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                Changed("ErrorMessage");
            }
        }

        public ICommand Save { get; set; }
        public ICommand Delete { get; set; }

        public EditNoteVM() {
            _data = DependencyLoader.Singleton<IDataManager>();
            _nav = DependencyLoader.Singleton<INavigationService>();
            _note = new NoteModel();
            Save = new ActionCommand(x => save());
            Delete = new ActionCommand(x => delete());
            IsEdit = false;
        }

        public EditNoteVM(NoteModel note)
            :this()
        {
            _note = note;
            IsEdit = true;
        }

        private async void save()
        {
            var user = _data.getLocalUser();
            RestResponse<NoteModel> response;
            if (IsEdit)
            {
                response = await _data.updateNoteRemote(user, _note);
            }
            else
            {
                response = await _data.saveNewNoteRemote(user, _note);
            }

            if (response.Success)
            {
                _nav.navigateNotes();
            }else
            {
                ErrorMessage = response.Message;
            }
            
        }

        private async void delete()
        {
            var user = _data.getLocalUser();
            var success = await _data.deleteNoteRemote(user, _note);
            if (!success)
            {
                //TODO: make success an RestResponse
            }
            _nav.navigateNotes();
        }
    }
}
