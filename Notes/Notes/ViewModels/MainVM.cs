using Notes.Data;
using Notes.Models;
using Notes.Services;
using Notes.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels
{
    public class MainVM : BaseViewModel
    {
        private IDataManager _data;
        private INavigationService _nav;

        public MainVM()
        {
            _data = DependencyLoader.New<IDataManager>();
            _nav = DependencyLoader.Singleton<INavigationService>();
            isLoggedIn();
        }

        public void isLoggedIn()
        {
            if(_data.getLocalUser() != null)
            {
                _nav.navigateNotes();
            }
            else
            {
                _nav.navigateLogin();
            }
        }
    }
}
