
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Mdichat.Droid.Services;
using Mdichat.Services;
using Console = System.Console;
using Environment = Android.OS.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace Mdichat.Droid.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveByteArrayAsImageFile(byte[] imageData, string fileName)
        {
            try
            {
                if (imageData == null || imageData.Length == 0) return string.Empty;

                var filename = System.IO.Path.Combine(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).ToString(), "NewFolder");
                Directory.CreateDirectory(filename);
                filename = System.IO.Path.Combine(filename, fileName + "-mdilarge.jpg");

                await SaveSmallImage(imageData, 200, 200, fileName);

                using (var fileOutputStream = new FileOutputStream(filename))
                {
                    await fileOutputStream.WriteAsync(imageData);
                    return filename;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task SaveSmallImage(byte[] imageData, float width, float height, string fileName)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            //var scale = (int)width / originalImage.Width;

            //var newWidth = originalImage.Width * scale;
            //var newHeight = originalImage.Height * scale;

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                var imageFile = System.IO.Path.Combine(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).ToString(), "NewFolder");
                Directory.CreateDirectory(imageFile);
                var smallImageFile = System.IO.Path.Combine(imageFile, fileName + "-mdismall.jpg");

                using (var fileOutputStream = new FileOutputStream(smallImageFile))
                {
                    await fileOutputStream.WriteAsync(ms.ToArray());
                }
            }
        }
    }
}