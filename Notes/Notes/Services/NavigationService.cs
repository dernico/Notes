using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Models;
using Xamarin.Forms;
using Notes.Views;
using Notes.Views.Modal;

[assembly: Dependency(typeof(Notes.Services.NavigationService))]
namespace Notes.Services
{
    public class NavigationService : INavigationService
    {
        private MasterDetailPage _masterPage;

        public NavigationService()
        {
        }

        public void navigateLogin()
        {
            navigatePage(new MasterPageItem
            {
                Title = "Login",
                IconSource = "",
                TargetType = typeof(LoginView)
            });
        }

        public void navigateNotes()
        {
            navigatePage(new MasterPageItem
            {
                Title = "Notes",
                IconSource = "",
                TargetType = typeof(NotesView)
            });
        }

        public void navigateSettings()
        {
            navigatePage(new MasterPageItem
            {
                Title = "Settings",
                IconSource = "",
                TargetType = typeof(SettingsView)
            });
        }

        public void navigateEditNote()
        {
            var page = new EditNoteView();
            page.Title = "Add";
            navigatePage(page);
        }

        public void navigateEditNote(NoteModel note)
        {
            var page = new EditNoteView(note);
            page.Title = "Edit";
            navigatePage(page);
        }


        public void navigatePage(MasterPageItem page)
        {
            if (page != null)
            {
                var newPage = (Page)Activator.CreateInstance(page.TargetType);
                newPage.Title = page.Title;
                navigatePage(newPage);
            }
        }

        public void navigatePage(Page page)
        {
            if (_masterPage == null) throw new Exception("Before using NavigationService call setMasterDetailPage(..)");
            if (page != null)
            {
                _masterPage.Detail = new NavigationPage(page);
                _masterPage.IsPresented = false;
            }
        }

        public void setMasterDetailPage(MasterDetailPage page)
        {
            _masterPage = page;
        }
    }
}
