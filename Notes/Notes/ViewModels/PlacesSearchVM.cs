using Notes.Data;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels
{
    public class PlacesSearchVM : BaseViewModel
    {
        private IDataManager _data;


        private string _input;
        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                autocomplete();
                Changed("Input");
            }
        }

        private List<AutoCompleteOption> _options;
        private List<AutoCompleteOption> Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
                Changed("Options");
            }
        }

        private async void autocomplete()
        {
            var user = _data.getLocalUser();
            var result = await _data.placesAutoComplete(user, Input);
            if (result.Success)
            {
                Options = result.ResponseObject;
            }
            else
            {
                //TODO: Error message?
            }
        }
    }
}
