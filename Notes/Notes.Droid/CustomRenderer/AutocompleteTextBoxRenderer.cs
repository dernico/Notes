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
using Android.Text;
using Java.Lang;

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

                Control.Text = Element.Text;
                Control.TextChanged += On_TextChanged;
                Control.ItemSelected += On_ItemSelected;
            }

            if(e.NewElement != null)
            {
                SetValues();
            }
        }

        private void On_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selected = Element.AutocompleteOptions[e.Position];
            Element.Text = selected.Description;
            Element.SelectedOption = selected;
        }

        private void On_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            SetText();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == AutocompleteTextBox.AutocompleteOptionsProperty.PropertyName)
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
                    Element.AutocompleteOptions.Select(a => a.Description).ToList());
                Control.Adapter = autoCompleteAdapter;
                
            }
            
        }

        private void SetText()
        {
            if (Control.Text == null) return;
            if (Element.Text == Control.Text) return;

            Element.Text = Control.Text;
        }
    }
}