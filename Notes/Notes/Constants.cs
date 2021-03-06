﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Constants
    {
#if DEBUG
        public static string HostAddress = "http://192.168.0.176:8000";
#else
        public static string HostAddress = "http://niconotes.herokuapp.com";
#endif
        public static string LoginAddress = HostAddress + "/login";
        public static string RegisterAddress = HostAddress + "/register";
        public static string NotesAddress = HostAddress + "/notes";
        public static string AddNoteAddress = HostAddress + "/addnote";
        public static string UpdateNoteAddress = HostAddress + "/updatenote";
        public static string DeleteNoteAddress = HostAddress + "/deletenote";
        public static string PlacesAutoCompleteAddress = HostAddress + "/places/autocomplete";
        public static string PlacesDetailsAdress = HostAddress + "/places/details";
    }
}
