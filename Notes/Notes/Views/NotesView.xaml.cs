﻿using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Notes.Views
{
    public partial class NotesView : ContentPage
    {
        public NotesView()
        {
            InitializeComponent();
            BindingContext = new NotesVM();
        }
    }
}
