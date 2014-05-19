using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TwoWayBindingSample
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        private Frame RootFrame
        {
            get { return Window.Current.Content as Frame; }
            set { Window.Current.Content = value;  }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            RootFrame = RootFrame ?? 
                    new Frame() { Language = ApplicationLanguages.Languages.First() };

            if (RootFrame.Content == null)
                RootFrame.Navigate(typeof(MainPage), e.Arguments);

            Window.Current.Activate();
        }
    }
}
