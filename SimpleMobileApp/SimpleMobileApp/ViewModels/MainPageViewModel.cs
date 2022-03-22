using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SimpleMobileApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Command SelectedNoteChangedCommand { get; }

        string selectedNote;
        public string SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedNote));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public MainPageViewModel()
        {
            AllNotes = new ObservableCollection<string>();

            SelectedNoteChangedCommand = new Command(async () =>
            {
                var detailVM = new DetailPageViewModel(SelectedNote);

                var detailPage = new DetailPage();

                detailPage.BindingContext = detailVM;

                await Application.Current.MainPage.Navigation.PushModalAsync(detailPage);
            });

            EraseCommand = new Command(() =>
            {
                TheNote = string.Empty;
            });

            SaveCommand = new Command(() =>
            {
                AllNotes.Add(TheNote);

                TheNote = string.Empty;
            });
        }

        public ObservableCollection<string> AllNotes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string theNote;

        public string TheNote
        {
            get { return theNote; }
            set 
            { 
                theNote = value;
                var args = new PropertyChangedEventArgs(nameof(TheNote));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command SaveCommand { get; }
        public Command EraseCommand { get; }

    }
}
