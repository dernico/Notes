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

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                Changed("ErrorMessage");
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

        private void resetErrorMessage()
        {
            ErrorMessage = String.Empty;
        }

        private async void login()
        {
            ShowLoading = true;
            resetErrorMessage();
            var response = await _data.loginRemote(Email, Password);
            if(response.Success)
            {
                int result = _data.storeLocalUser(response.ResponseObject);
                _nav.navigateNotes();
            }
            else
            {
                ErrorMessage = response.Message;
            }
            
            ShowLoading = false;
        }

        private async void register()
        {
            ShowLoading = true;
            resetErrorMessage();
            RestResponse<string> resp = await _data.registerRemote(Email, Password);
            if (resp.Success)
            {
                login();
            }
            else
            {
                ErrorMessage = resp.Message;
            }
            ShowLoading = false;
        }

    }
}
