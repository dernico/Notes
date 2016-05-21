using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.Services
{
    public interface INavigationService
    {
        void setMasterDetailPage(MasterDetailPage page);
        void navigateLogin();
        void navigateNotes();
        void navigateEditNote();
        void navigateEditNote(NoteModel note);
        void navigateSettings();
        void navigatePage(Page page);
        void navigatePage(MasterPageItem page);
    }
}
