using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.CustomRenderer
{
    public class AutocompleteTextBox : Entry
    {
        public static readonly BindableProperty AutocompleteOptionsProperty =
            BindableProperty.Create(
            "AutocompleteOptions",
            typeof(List<string>),
            typeof(AutocompleteTextBox),
            new List<string>());

        public List<string> AutocompleteOptions
        {
            set { SetValue(AutocompleteOptionsProperty, value); }
            get { return (List<string>)GetValue(AutocompleteOptionsProperty); }
        }
    }
}
