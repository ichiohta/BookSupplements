using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DumpTileTemplates
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Window current = Window.Current;

            Frame frame =
                current.Content as Frame ??
                (current.Content = new Frame() { Language = ApplicationLanguages.Languages[0] }) as Frame;


            if (frame.Content == null)
                frame.Navigate(typeof(MainPage), e.Arguments);

            current.Activate();
        }
    }
}
