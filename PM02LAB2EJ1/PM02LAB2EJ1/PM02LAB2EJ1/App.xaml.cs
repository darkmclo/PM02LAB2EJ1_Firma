//Christian André Alvarado Reyes - 201910010261
using PM02LAB2EJ1.Controllers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poppins-Regular.ttf", Alias = "Poppins")]
[assembly: ExportFont("Poppins-Bold.ttf", Alias = "Poppins Bold")]
[assembly: ExportFont("Poppins-Italic.ttf", Alias = "Poppins Italic")]
namespace PM02LAB2EJ1
{
    public partial class App : Application
    {
        static DatabaseSQLite dbsqlite;
        public static DatabaseSQLite BaseDatos
        {
            get
            {
                if (dbsqlite == null)
                {
                    dbsqlite = new DatabaseSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM02LAB2EJ1.db3"));
                }
                return dbsqlite;
            }

        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.InitPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
