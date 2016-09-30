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
using Android.Content.Res;
using Android.Util;

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
                Control.ItemClick += On_ItemClick; ;
            }

            if(e.NewElement != null)
            {
                SetValues();
            }
        }

        private void On_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
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
                var autoCompleteAdapter = new CoolAdapter(Context, 
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

    //TODO: Please do alot of refactoring here soon!
    public class CoolAdapter : BaseAdapter, IFilterable, IAdapter
    {
        private readonly Context _context;
        private readonly int _resource;
        private readonly System.Collections.IList _objects;
        private readonly LayoutInflater _inflater;


        public CoolAdapter(Context context, int resource, System.Collections.IList objects)
        {
            _context = context;
            _resource = resource;
            _objects = objects;
            _inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }

        public override int Count
        {
            get
            {
                if(_objects == null)
                {
                    return 0;
                }
                return _objects.Count;
            }
        }

        public Filter Filter
        {
            get
            {
                return new ContainsFilter(_objects);
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            if(position > _objects.Count || position < 0)
            {
                return null;
            }
            return _objects[position].ToString();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View view;
            TextView text;

            if (convertView == null)
            {
                view = _inflater.Inflate(_resource, parent, false);
            }
            else
            {
                view = convertView;
            }
            
            try
            {
                text = (TextView)view;
            }
            catch (ClassCastException e)
            {
                Log.Error("ArrayAdapter",
                        "You must supply a resource ID for a TextView");
                throw new IllegalStateException(
                        "ArrayAdapter requires the resource ID to be a TextView", e);
            }

            var item = GetItem(position);

            if (item != null) {
                text.SetText(item.ToString(), TextView.BufferType.Normal);
            }

            return view;
        }

        private class ContainsFilter : Filter
        {
            private System.Collections.IList _listObjects;
            private static object _lock = new object();

            public ContainsFilter(System.Collections.IList objects)
            {
                _listObjects = objects;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                FilterResults results = new FilterResults();
                lock (_lock)
                {

                    if (_listObjects == null)
                    {
                        return results;
                    }

                    if (constraint == null || constraint.Length() == 0)
                    {
                        results.Values = _listObjects as Java.Lang.Object;
                        results.Count = _listObjects.Count;
                    }
                    else
                    {
                        string searchString = constraint.ToString().ToLowerInvariant();
                        System.Collections.IList resulList = new System.Collections.ArrayList();

                        foreach (var listItem in _listObjects)
                        {
                            var listItemString = listItem.ToString().ToLowerInvariant();

                            var searchParts = searchString.Split(' ');

                            var containsCount = 0;
                            foreach (var part in searchParts)
                            {
                                if (listItemString.Contains(part))
                                {
                                    containsCount++;
                                }
                            }

                            if (containsCount == searchParts.Length)
                            {
                                resulList.Add(listItem);
                            }
                        }

                        results.Values = resulList as Java.Lang.Object;
                        results.Count = resulList.Count;
                    }
                }

                return results;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                
            }
        }

    }
}