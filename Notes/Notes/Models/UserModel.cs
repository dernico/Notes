using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int LocalID { get; set; }

        public string _id { get; set; }
        public string token { get; set; }
        public string email { get; set; }
    }
}
