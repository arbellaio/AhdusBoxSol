using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using AhdusBoxSol.Models;
using AhdusBoxSol.Resources;
using AhdusBoxSol.Views;
using Plugin.Media.Abstractions;
using PulseXLibraries.Helpers.Alert;
using PulseXLibraries.Helpers.CommandLocker;
using PulseXLibraries.Helpers.FirebaseFileUpload;
using PulseXLibraries.Helpers.FirebaseRealTime;
using PulseXLibraries.Helpers.Media;
using PulseXLibraries.Models.Firebase;
using PulseXLibraries.ViewModels.Base;
using PulseXLibraries.Views.BaseApp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AhdusBoxSol.ViewModels.Detail
{
    public class DetailViewModel : BaseViewModel
    {
        private FirebaseItem<Box> _selectedBox;

        public FirebaseItem<Box> SelectedBox
        {
            get => _selectedBox;
            set
            {
                _selectedBox = value;
                OnPropertyChanged(nameof(SelectedBox));
            }
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public ICommand SaveBoxCommand => new Command(SaveBoxCommandLocker.Execute);
        private CommandLockerHelper SaveBoxCommandLocker => new CommandLockerHelper(async () => { SaveBoxInfo(); });

        public ICommand SelectFileCommand => new Command(SelectFileCommandLocker.Execute);
        private CommandLockerHelper SelectFileCommandLocker => new CommandLockerHelper(async () => { SelectFile(); });

        private async void SelectFile()
        {
            try
            {
                var mediaPicker = new MediaPickerHelper();
                var mediaFile = await mediaPicker.MediaSelection();
                var stream = mediaFile.GetStream();
              
                // var memoryStream = new MemoryStream();
                // await stream.CopyToAsync(memoryStream);
                    var filePath = await FirebaseFileUploadHelper.UploadFileToFirebase(Guid.NewGuid().ToString()+".jpg",
                    stream, AppConstants.ApiKey,
                    AppConstants.FirebaseFileStorageUrl, AppConstants.Email, AppConstants.Password);
                FilePath = await filePath;
                await AlertHelper.ShowAlert("Alert", "File Uploaded");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override Task OnAppearing()
        {
            if (_selectedBox == null)
                _selectedBox = new FirebaseItem<Box>
                {
                    Item = new Box(),
                };
            return base.OnAppearing();
        }

        private async void SaveBoxInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedBox.StringId))
                {
                    SelectedBox.Item.ImagePath = FilePath;
                    SelectedBox.StringId = Guid.NewGuid().ToString();
                    SelectedBox.Item.Id = SelectedBox.StringId;
                    await FirebaseRealDbHelper.AddToFirebase(SelectedBox, "Boxes");
                }
                else
                {
                    SelectedBox.Item.ImagePath = FilePath;
                    await FirebaseRealDbHelper.UpdateItemByStringId(SelectedBox, "Boxes");
                }

                await BaseApp.Navigation.PopAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}