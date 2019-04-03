using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwipeTest.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }

        private ICommand _addToFavoritesCommand;
        public ICommand AddToFavoritesCommand => _addToFavoritesCommand ?? (_addToFavoritesCommand = new Command<MainViewModel>(m =>
            Debug.WriteLine($"\n\n[TEST] {m.Text1}, {m.Text2}\n\n")));
    }

    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<MainViewModel> _items;
        public ObservableCollection<MainViewModel> Items { get => _items; set => SetProperty(ref _items, value); }

        public MainPageViewModel()
        {
            Items = new ObservableCollection<MainViewModel>
            {
                new MainViewModel { Text1 = "Test 1.1", Text2 = "Test 1.2" },
                new MainViewModel { Text1 = "Test 2.1", Text2 = "Test 2.2" },
                new MainViewModel { Text1 = "Test 3.1", Text2 = "Test 3.2" },
                new MainViewModel { Text1 = "Test 4.1", Text2 = "Test 4.2" },
                new MainViewModel { Text1 = "Test 5.1", Text2 = "Test 5.2" },
            };
        }
    }
}
