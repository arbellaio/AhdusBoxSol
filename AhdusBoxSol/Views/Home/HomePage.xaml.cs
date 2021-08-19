using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AhdusBoxSol.Models;
using AhdusBoxSol.ViewModels.Home;
using AhdusBoxSol.Views.Detail;
using Firebase.Database;
using PulseXLibraries.Helpers.Alert;
using PulseXLibraries.Helpers.FirebaseRealTime;
using PulseXLibraries.Models.Firebase;
using PulseXLibraries.Views.Base;
using PulseXLibraries.Views.BaseApp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AhdusBoxSol.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage
    {
        public HomeViewModel ViewModel => BindingContext as HomeViewModel;
        public HomePage()
        {
            InitializeComponent();
        }

        private async void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.CurrentSelection != null)
                {
                    var item = e.CurrentSelection[0];
                    if (item != null)
                    {
                        var firebaseObject = item as  FirebaseObject<FirebaseItem<Box>>;
                        var isShowDetail = await AlertHelper.ShowAlertResponse("Alert", "Please select appropriate option", "Delete", "View");
                        if (isShowDetail)
                        {
                            await BaseApp.Navigation.PushAsync(new DetailPage(firebaseObject?.Object));
                        }
                        else
                        {
                            await FirebaseRealDbHelper.DeleteItemByStringId(firebaseObject?.Object, "Boxes");
                            ViewModel.GetAllBoxes();
                        }
                    }
                    
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        
    }
}