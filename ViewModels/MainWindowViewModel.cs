using CommunityToolkit.Mvvm.ComponentModel;
using QuestionApp1.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuestionApp1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<VolumeSetting> VolumeSettings { get; }

        [ObservableProperty]
        public VolumeSetting? selectedVolumeSetting = null;

        partial void OnSelectedVolumeSettingChanged(VolumeSetting? value)
        {
            AnyRowIsSelected = (value != null);
        }

        [ObservableProperty]
        public bool anyRowIsSelected = false;

        public MainWindowViewModel()
        {
            var volumeSettings = new List<VolumeSetting>
            {
                new VolumeSetting(25),
                new VolumeSetting(50),
                new VolumeSetting(75)
            };
            VolumeSettings = new ObservableCollection<VolumeSetting>(volumeSettings);
        }
    }
}
