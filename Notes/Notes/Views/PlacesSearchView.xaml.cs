using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Notes.Views
{
    public partial class PlacesSearchView : ContentPage
    {
        public PlacesSearchView()
        {
            InitializeComponent();
            BindingContext = new PlacesSearchVM();
        }
    }
}
