using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Local
{
    public interface ISQLite
    {
        SQLiteConnection getConnection();
    }
}
