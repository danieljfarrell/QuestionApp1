# What is the recommend way of editing a model value from multiple controls?

![Alt text](screenshot.jpg "UI")

This example is a little contrived but it illustrates the point. It is an app that can toggle between three preset volume levels by clicking the row in the data grid and allow editing of the selected value.

This uses Avalonia and the MVVM Community Toolkit.

Main Window View Model contains a model object, in fact an observable collection of model objects,


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

The model object is as simple as could be, just containing one property

    namespace QuestionApp1.Models
    {
        public class VolumeSetting
        {
            public int Volume { set; get; }

            public VolumeSetting(int volume)
            {
                Volume = volume;
            }
        }
    }

All the UI is in the MainWindow.axaml

    <Window xmlns="https://github.com/avaloniaui"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:vm="using:QuestionApp1.ViewModels"
		    xmlns:m="using:QuestionApp1.Models"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
            x:Class="QuestionApp1.Views.MainWindow"
            x:DataType="vm:MainWindowViewModel"
            Icon="/Assets/avalonia-logo.ico"
            Title="QuestionApp1">

        <Design.DataContext>
            <vm:MainWindowViewModel/>
        </Design.DataContext>

	    <StackPanel Margin="20">
		    <Label>Volume:</Label>
		    <Slider Value="{Binding SelectedVolumeSetting.Volume}" IsEnabled="{Binding AnyRowIsSelected}"></Slider>
		    <NumericUpDown FormatString="N0" Minimum="0" Maximum="100" Increment="5" Value="{Binding SelectedVolumeSetting.Volume}" IsEnabled="{Binding AnyRowIsSelected}"></NumericUpDown>
		    <DataGrid Margin="20" Height="200" ItemsSource="{Binding VolumeSettings}" SelectedItem="{Binding SelectedVolumeSetting}"
				      AutoGenerateColumns="False" IsReadOnly="False"
				      GridLinesVisibility="All"
				      BorderThickness="1" BorderBrush="Gray">
			    <DataGrid.Columns>
				    <DataGridTemplateColumn Header="Volume">
					    <DataGridTemplateColumn.CellTemplate>
						    <DataTemplate DataType="m:VolumeSetting">
							    <TextBlock Text="{Binding Volume, StringFormat='{}{0}'}"
							      VerticalAlignment="Center" HorizontalAlignment="Center" />
						    </DataTemplate>
					    </DataGridTemplateColumn.CellTemplate>
					    <DataGridTemplateColumn.CellEditingTemplate>
						    <DataTemplate DataType="m:VolumeSetting">
							    <NumericUpDown Value="{Binding Volume}"
							       FormatString="N0" Minimum="0" Maximum="100" Increment="10"
							       HorizontalAlignment="Stretch"/>
						    </DataTemplate>
					    </DataGridTemplateColumn.CellEditingTemplate>
				    </DataGridTemplateColumn>
			    </DataGrid.Columns>
		    </DataGrid>
	    </StackPanel>
    </Window>

## Problems

1. When the slider edit the value the other control to not update
2. When the numerical up and down edits the value the other control to not update.
3. When editing the value in the table value the other controls to not update.

