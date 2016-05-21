using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Local
{
    public interface INotesDatabase
    {
        UserModel getUser();
        int addUser(UserModel user);
        void clearUser();
    }
}
