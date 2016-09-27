using Notes.CustomRenderer;
using Notes.UWP.CustomRenderer;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using Notes.Models;
using System.Linq;

[assembly: ExportRenderer(typeof(AutocompleteTextBox), typeof(AutoCompleteTextBoxRenderer))]
namespace Notes.UWP.CustomRenderer
{
    public class AutoCompleteTextBoxRenderer : ViewRenderer<AutocompleteTextBox, AutoSuggestBox>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<AutocompleteTextBox> e)
        {
            base.OnElementChanged(e);

            if(Control == null && e.NewElement != null)
            {
                SetNativeControl(new AutoSuggestBox());
                
                Control.Text = Element.Text;
                
                Control.TextChanged += Control_TextChanged;
                Control.SuggestionChosen += Control_SuggestionChosen;
                SetAutoCompleteValues();
            }
        }

        private void Control_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selected = args.SelectedItem.ToString();
            Element.Text = selected;
            Element.SelectedOption = Element.AutocompleteOptions.FirstOrDefault(a => a.Description == selected);
        }

        private void Control_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                SetText();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AutocompleteTextBox.AutocompleteOptionsProperty.PropertyName)
            {
                SetAutoCompleteValues();
            }
        }


        private void SetAutoCompleteValues()
        {
            if (Element.AutocompleteOptions != null)
            {
                Control.ItemsSource = Element.AutocompleteOptions.Select(a => a.Description);
            }

        }

        private void SetText()
        {
            if(Control.Text == null)
            {
                return;
            }


            if (Element.Text != Control.Text)
            {
                Element.Text = Control.Text;
            }
        }
    }
}
