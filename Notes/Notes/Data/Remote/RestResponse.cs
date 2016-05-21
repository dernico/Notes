﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Remote
{
    public class RestResponse <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T ResponseObject { get; set; }
    }
}
