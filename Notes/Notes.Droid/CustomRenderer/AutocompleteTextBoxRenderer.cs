using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Notes.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Notes.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(AutocompleteTextBox), typeof(AutocompleteTextBoxRenderer))]
namespace Notes.Droid.CustomRenderer
{
    class AutocompleteTextBoxRenderer : ViewRenderer<AutocompleteTextBox, AutoCompleteTextView>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<AutocompleteTextBox> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {
                SetNativeControl(new AutoCompleteTextView(Context));
                Control.TextChanged += Control_TextChanged;
            }

            if(e.NewElement != null)
            {
                SetValues();
            }
        }

        private void Control_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            Element.Text = Control.Text;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == AutocompleteTextBox.AutocompleteOptionsProperty.PropertyName)
            {
                SetValues();
            }
            if(e.PropertyName == AutocompleteTextBox.TextProperty.PropertyName)
            {
                SetValues();
            }
        }

        private void SetValues()
        {
            if(Element.AutocompleteOptions != null)
            {
                ArrayAdapter autoCompleteAdapter = new ArrayAdapter(Context, 
                    Android.Resource.Layout.SimpleDropDownItem1Line,
                    Element.AutocompleteOptions);
                Control.Adapter = autoCompleteAdapter;
            }
            
        }
    }
}