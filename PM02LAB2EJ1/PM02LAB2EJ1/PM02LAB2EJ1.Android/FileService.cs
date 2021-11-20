using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PM02LAB2EJ1.Droid;
using PM02LAB2EJ1.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace PM02LAB2EJ1.Droid
{
    public class FileService : IFileService
    {
        public bool SavePicture(string filename, Stream data)
        {
            if (MainActivity.Instance.CheckSelfPermission(Manifest.Permission.WriteExternalStorage) == Android.Content.PM.Permission.Granted)
            {
                //System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                //var documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var documentsPath = Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath;
                documentsPath = Path.Combine(documentsPath, "Firmas_Xamarin");
                Directory.CreateDirectory(documentsPath);

                string filePath = Path.Combine(documentsPath, filename);

                byte[] bArray = new byte[data.Length];
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (data)
                    {
                        data.Read(bArray, 0, (int)data.Length);
                    }
                    int length = bArray.Length;
                    fs.Write(bArray, 0, length);
                }
                return true;
            }
            else
            {
                MainActivity.Instance.RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
                return false;
            }
        }

        public string GetPicturePath()
        {
            var documentsPath = Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath;
            documentsPath = Path.Combine(documentsPath, "Firmas_Xamarin");
            return documentsPath;
        }
    }
}