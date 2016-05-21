using Notes.Data;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notes.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        private INavigationService _nav;
        private IDataManager _data;

        public ICommand Logout { get; set; }

        public SettingsVM()
        {
            _nav = DependencyLoader.Singleton<INavigationService>();
            _data = DependencyLoader.Singleton<IDataManager>();
            Logout = new ActionCommand(x => logoutAndLogin());
        }

        private void logoutAndLogin()
        {
            _data.clearLocalUser();
            _nav.navigateLogin();
        }
    }
}
