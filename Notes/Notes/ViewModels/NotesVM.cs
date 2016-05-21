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
    public class NotesVM : BaseViewModel
    {
        private IDataManager _data;
        private INavigationService _nav;

        private List<NoteModel> _notes;
        public List<NoteModel> Notes
        {
            get
            {
                if (_notes == null)
                {
                    loadNotes();
                }
                return _notes;
            }
            set
            {
                _notes = value;
                Changed("Notes");
            }
        }

        private NoteModel _selectedNote;
        public NoteModel SelectedNote {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value;
                if(_selectedNote != null)
                {
                    _nav.navigateEditNote(_selectedNote);
                }
                
            }
        }

        public ICommand AddNote { get; set; }

        public NotesVM()
        {
            _data = DependencyLoader.Singleton<IDataManager>();
            _nav = DependencyLoader.Singleton<INavigationService>();
            AddNote = new ActionCommand(x => _nav.navigateEditNote());
        }

        private async void loadNotes()
        {
            var user = _data.getLocalUser();
            var notes = await _data.loadUserNotes(user);
            Notes = notes;
        }
    }
}
