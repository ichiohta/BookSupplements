using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DumpTileTemplates
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Click_Save(object sender, RoutedEventArgs e)
        {
            Func<StorageFolder, TileTemplateType, Task> saveTemplate =
                async (f, t) =>
                {
                    XmlDocument content = TileUpdateManager.GetTemplateContent(t);
                    var node = content.GetElementsByTagName("binding").First();

                    string name = node.Attributes.Single(a => a.LocalName.ToString() == "template").NodeValue.ToString();

                    StorageFile file = await f.CreateFileAsync(string.Format("{0}.xml", name), CreationCollisionOption.ReplaceExisting);
                    await content.SaveToFileAsync(file);
                };

            var picker =
                new FolderPicker()
                {
                    SuggestedStartLocation = PickerLocationId.Desktop
                };

            picker.FileTypeFilter.Add(".xml");

            StorageFolder rootFolder = await picker.PickSingleFolderAsync();
            StorageFolder folder = await rootFolder.CreateFolderAsync(
                "Tile Templates", CreationCollisionOption.GenerateUniqueName);

            ((TileTemplateType[])Enum.GetValues(typeof(TileTemplateType)))
                .Distinct()
                .Select(type => saveTemplate(folder, type))
                .ToArray();
        }

        private void UpdateTile_Click(object sender, RoutedEventArgs e)
        {
            // 更新情報として表示するテキスト
            string text = TileText.Text;

            // XMLの生成
            var content =
                new XDocument(
                    new XElement(
                        "tile",
                        new XElement(
                            "visual",
                            new XAttribute("version", "2"),
                            new XElement(
                                "binding",
                                new XAttribute("template", "TileSquare150x150Text01"),
                                new XAttribute("fallback", "TileSquareText01"),
                                new XElement("text", new XAttribute("id", "1"), new XText(text))),
                            new XElement(
                                "binding",
                                new XAttribute("template", "TileSquare310x310Text01"),
                                new XElement("text", new XAttribute("id", "1"), new XText(text))),
                            new XElement(
                                "binding",
                                new XAttribute("template", "TileWide310x150Text01"),
                                new XElement("text", new XAttribute("id", "1"), new XText(text)))
                                )));

            // XDocument → XmlDocument
            var document = new XmlDocument();
            document.LoadXml(content.ToString(SaveOptions.DisableFormatting));

            // ライブタイルを更新
            TileUpdateManager
                .CreateTileUpdaterForApplication()
                .Update(new TileNotification(document));
        }
    }
}
