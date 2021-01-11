using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TesteDrive.Media;
using TesteDrive.Droid;
using Xamarin.Forms;
using Android.Content;
using Android.Provider;
using Java.IO;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]

namespace TesteDrive.Droid
{
    [Activity(Label = "TesteDrive", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        public static File file;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            MessagingCenter.Send<File>(file, "ImagemCapturada");

        }

        [Obsolete]
        public void TirarFoto()
        {
            var actvity = Forms.Context as Activity;
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            file = GetImagem();

            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));

            actvity.StartActivityForResult(intent, 0);
        }

        private static File GetImagem()
        {
            File file;
            Java.IO.File diretorio = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Imagens");

            if (!diretorio.Exists())
                diretorio.Mkdirs();

            file = new Java.IO.File(diretorio, "fototeste.png");
            return file;
        }
    }
}