using Notes.Models;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Notes.Views
{
    public partial class NavigationView : ContentPage
    {
        public NavigationView()
        {
            InitializeComponent();
            var navigation = new NavigationVM();
            BindingContext = navigation;
        }
    }
}
