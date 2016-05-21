using Notes.Models;
using Notes.Services;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Notes.Views
{
    public partial class MainView : MasterDetailPage
    {
        private NavigationView _navigation;
        public MainView()
        {
            InitializeComponent();

            _navigation = new NavigationView();
            Master = _navigation;
            Detail = new NavigationPage(new SettingsView());

            DependencyLoader.Singleton<INavigationService>().setMasterDetailPage(this);

            var mainVM = new MainVM();
            BindingContext = mainVM;

        }
    }
}
