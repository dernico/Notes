using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class NoteModel
    {
        [PrimaryKey, AutoIncrement]
        public int LocalID { get; set; }

        public string _id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
