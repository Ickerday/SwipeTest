using SwipeTest.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace SwipeTest
{
    [DesignTimeVisible(true)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (!(sender is StackLayout listItem) || !(listItem.BindingContext is MainViewModel viewModel))
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                case GestureStatus.Running:
                    listItem.TranslationX = e.TotalX;
                    listItem.TranslationY = 0;
                    break;

                case GestureStatus.Canceled:
                    listItem.TranslationX = 0;
                    listItem.TranslationY = 0;
                    break;

                case GestureStatus.Completed:
                    listItem.TranslationX = 0;
                    listItem.TranslationY = 0;

                    viewModel.AddToFavoritesCommand.Execute(viewModel);
                    break;
            }
        }
    }
}
