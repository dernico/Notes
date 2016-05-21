using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Constants
    {
        //public static string HostAddress = "http://10.6.0.35:8000";
        public static string HostAddress = "http://niconotes.herokuapp.com";
        public static string LoginAddress = HostAddress + "/login";
        public static string RegisterAddress = HostAddress + "/register";
        public static string NotesAddress = HostAddress + "/notes";
        public static string AddNoteAddress = HostAddress + "/addnote";
        public static string DeleteNoteAddress = HostAddress + "/deletenote";
    }
}
