using Windows.UI.Xaml.Controls;

namespace TwoWayBindingSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext =
                new Book()
                {
                    Title = "Windows 8本格プログラミング入門",
                    Price = 2800
                };
        }
    }
}
