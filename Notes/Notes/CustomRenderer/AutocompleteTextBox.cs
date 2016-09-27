using Notes.Models;
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
            typeof(List<AutoCompleteOption>),
            typeof(AutocompleteTextBox),
            new List<AutoCompleteOption>());

        public List<AutoCompleteOption> AutocompleteOptions
        {
            set { SetValue(AutocompleteOptionsProperty, value); }
            get { return (List<AutoCompleteOption>)GetValue(AutocompleteOptionsProperty); }
        }

        public static readonly BindableProperty SelectedOptionProperty = BindableProperty.Create(
            "SelectedOption",
            typeof(AutoCompleteOption),
            typeof(AutocompleteTextBox),
            null,
            BindingMode.TwoWay);

        public AutoCompleteOption SelectedOption
        {
            set { SetValue(SelectedOptionProperty, value); }
            get { return (AutoCompleteOption)GetValue(SelectedOptionProperty); }
        }
    }
}
