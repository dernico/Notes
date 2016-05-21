using Notes.Models;
using Notes.Services;
using Notes.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NavigationVM : BaseViewModel
    {
        private INavigationService _navigation;

        private List<MasterPageItem> _navigationItems;
        public List<MasterPageItem> NavigationItems {
            get
            {
                if(_navigationItems == null)
                {
                    _navigationItems = new List<MasterPageItem> {
                        new MasterPageItem
                        {
                            Title = "Notes",
                            IconSource = "",
                            TargetType = typeof(NotesView)
                        },
                        new MasterPageItem
                        {
                            Title = "Settings",
                            IconSource = "",
                            TargetType = typeof(SettingsView)
                        }
                    };
                }
                return _navigationItems;
            }
            private set { }
        }

        private MasterPageItem _selectedNavigationItem;
        public MasterPageItem SelectedNavigationItem {
            get
            {
                return _selectedNavigationItem;
            }
            set
            {
                _selectedNavigationItem = value;
                _navigation.navigatePage(_selectedNavigationItem);
                //_selectedNavigationItem = null;
                Changed("SelectedNavigationItem");
            }
        }

        public NavigationVM()
        {
            _navigation = DependencyService.Get<INavigationService>();
        }
    }
}
