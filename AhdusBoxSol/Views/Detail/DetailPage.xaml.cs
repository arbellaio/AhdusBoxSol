using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AhdusBoxSol.Models;
using AhdusBoxSol.ViewModels.Detail;
using PulseXLibraries.Models.Firebase;
using PulseXLibraries.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AhdusBoxSol.Views.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : BasePage
    {
        public DetailViewModel ViewModel => BindingContext as DetailViewModel;
        public DetailPage(FirebaseItem<Box> firebaseItem = null)
        {
            InitializeComponent();
            if (firebaseItem != null)
            {
                ViewModel.SelectedBox = firebaseItem;
            }
        }
    }
}