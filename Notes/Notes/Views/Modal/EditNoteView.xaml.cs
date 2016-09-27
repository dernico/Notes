using Notes.CustomRenderer;
using Notes.Models;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Notes.Views.Modal
{
    public partial class EditNoteView : ContentPage
    {
        public EditNoteView()
            : this(null)
        {
        }

        public EditNoteView(NoteModel note)
        {
            InitializeComponent();
            if (note == null)
            {
                BindingContext = new EditNoteVM();
            }else
            {
                BindingContext = new EditNoteVM(note);
            }
        }
    }
}
