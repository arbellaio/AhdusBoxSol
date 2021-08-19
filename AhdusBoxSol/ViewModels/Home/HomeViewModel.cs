using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AhdusBoxSol.Models;
using AhdusBoxSol.Resources;
using AhdusBoxSol.Views.Detail;
using Firebase.Database;
using PulseXLibraries.Helpers.CommandLocker;
using PulseXLibraries.Helpers.FirebaseRealTime;
using PulseXLibraries.Models.Firebase;
using PulseXLibraries.ViewModels.Base;
using PulseXLibraries.Views.BaseApp;
using Xamarin.Forms;

namespace AhdusBoxSol.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand AddBoxCommand => new Command(AddBoxCommandLocker.Execute);
        private CommandLockerHelper AddBoxCommandLocker => new CommandLockerHelper(async () => { GoToAddBoxPage(); });

        private ObservableCollection<FirebaseObject<FirebaseItem<Box>>> _boxes;
        public ObservableCollection<FirebaseObject<FirebaseItem<Box>>> Boxes
        {
            get => _boxes;
            set
            {
                _boxes = value;
                OnPropertyChanged(nameof(Boxes));
            }
        }

        private FirebaseObject<FirebaseItem<Box>> _selectedBox;
        public FirebaseObject<FirebaseItem<Box>> SelectedBox
        {
            get => _selectedBox;
            set
            {
                _selectedBox = value;
                OnPropertyChanged(nameof(SelectedBox));
            }
        }
        public override Task OnAppearing()
        {
            ConnectToFirebase();
            GetAllBoxes();
            return base.OnAppearing();
        }

        public async void GetAllBoxes()
        {
            try
            {
                IsLoading = true;
                var boxes = await FirebaseRealDbHelper.GetAllItems<FirebaseItem<Box>>("Boxes");
                Boxes = new ObservableCollection<FirebaseObject<FirebaseItem<Box>>>(boxes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsLoading = false;
            }
 
        }
        private bool ConnectToFirebase()
        {
            return FirebaseRealDbHelper.ConnectFirebase(AppConstants.FirebaseDatabaseUrl);
        }
        
        private async void GoToAddBoxPage()
        {
            await BaseApp.Navigation.PushAsync(new DetailPage());
        }

    }
}