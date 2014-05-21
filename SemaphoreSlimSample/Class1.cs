using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace SemaphoreSlimSample
{
    public static class StorageUtility
    {
        private static SemaphoreSlim Semaphore = new SemaphoreSlim(initialCount: 0, maxCount: 1);

        private static Collection<string> NameList = new Collection<string>();

        public static async Task Add(string name)
        {
            await Semaphore.WaitAsync();

            var folder = ApplicationData.Current.LocalFolder;

            using (var stream =
                await folder.OpenStreamForWriteAsync(
                    "namelist.xml", CreationCollisionOption.ReplaceExisting))
            {
                new DataContractSerializer(typeof(Collection<string>))
                    .WriteObject(stream, NameList);
            }

            Semaphore.Release();
        }
    }
}
