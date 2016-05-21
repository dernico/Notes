using Notes.Data;
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
            if (IsEdit)
            {
                _note = await _data.updateNoteRemote(user, _note);
            }
            else
            {
                _note = await _data.saveNewNoteRemote(user, _note);
            }
            _nav.navigateNotes();
        }

        private void delete()
        {
            var user = _data.getLocalUser();
            var success = _data.deleteNoteRemote(user, _note);
            _nav.navigateNotes();
        }
    }
}
