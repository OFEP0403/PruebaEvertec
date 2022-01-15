
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test.espinosa_oscar.evertec.Views
{
    public partial class TipsListView : ContentPage
    {
        public TipsListView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.TipsListViewModel(Navigation);            
        }
    }
}
