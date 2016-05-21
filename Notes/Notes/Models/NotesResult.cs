using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class NotesResult
    {
        public string error { get; set; }
        public List<NoteModel> notes { get; set; }
    }
}
