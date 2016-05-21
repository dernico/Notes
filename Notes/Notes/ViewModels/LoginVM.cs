using Notes.Data;
using Notes.Data.Remote;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notes.ViewModels
{
    public class LoginVM : BaseViewModel
    {
        private string _email;
        public string Email {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                Changed("Email");
            }
        }

        private string _password;
        public string Password {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                Changed("Password");
            }
        }


        private bool _showLoading;
        public bool ShowLoading {
            get
            {
                return _showLoading;
            }
            set
            {
                _showLoading = value;
                Changed("ShowLoading");
            }
        }

        public ICommand Login { get; set; }
        public ICommand Register { get; set; }

        private IDataManager _data;
        private INavigationService _nav;

        public LoginVM()
        {
            Login = new ActionCommand(x => login());
            Register = new ActionCommand(x => register());
            _data = DependencyLoader.Singleton<IDataManager>();
            _nav = DependencyLoader.Singleton<INavigationService>();
        }

        public async void login()
        {
            ShowLoading = true;
            var user = await _data.loginRemote(Email, Password);
            if(user != null)
            {
                int result = _data.storeLocalUser(user);
            }
            
            ShowLoading = false;
            _nav.navigateNotes();
        }

        public async void register()
        {
            ShowLoading = true;
            RestResponse<string> resp = await _data.registerRemote(Email, Password);
            if (resp.Success)
            {
                login();
            }
            ShowLoading = false;
        }

    }
}
