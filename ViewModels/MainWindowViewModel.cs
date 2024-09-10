using CommunityToolkit.Mvvm.ComponentModel;
using QuestionApp1.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuestionApp1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        // For Non-Observable model objects
        public ObservableCollection<VolumeSetting> VolumeSettings { get; }

        [ObservableProperty]
        public VolumeSetting? selectedVolumeSetting = null;

        partial void OnSelectedVolumeSettingChanged(VolumeSetting? value)
        {
            AnyRowIsSelected = (value != null);
        }

        [ObservableProperty]
        public bool anyRowIsSelected = false;


        // For Observable model objects

        public ObservableCollection<ObservableVolumeSetting> ObservableVolumeSettings { get; }

        [ObservableProperty]
        public ObservableVolumeSetting? selectedObservableVolumeSetting = null;

        partial void OnSelectedObservableVolumeSettingChanged(ObservableVolumeSetting? value)
        {
            AnyRowIsSelectedObservable = (value != null);
        }

        [ObservableProperty]
        public bool anyRowIsSelectedObservable = false;



        public MainWindowViewModel()
        {
            var volumeSettings = new List<VolumeSetting>
            {
                new VolumeSetting(25),
                new VolumeSetting(50),
                new VolumeSetting(75)
            };
            VolumeSettings = new ObservableCollection<VolumeSetting>(volumeSettings);

            var observableVolumeSettings = new List<ObservableVolumeSetting>
            {
                new ObservableVolumeSetting(25),
                new ObservableVolumeSetting(50),
                new ObservableVolumeSetting(75)
            };
            ObservableVolumeSettings = new ObservableCollection<ObservableVolumeSetting>(observableVolumeSettings);
        }
    }
}
