using PM02LAB2EJ1.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace PM02LAB2EJ1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitPage : ContentPage
    {
        public InitPage()
        {
            InitializeComponent();
            PermissionStorageWrite();
        }

        string base64Val;

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Length > 0 && txtDescripcion.Text.Length > 0)
                {
                    var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                    var mStream = (MemoryStream)image;
                    byte[] data = mStream.ToArray();
                    base64Val = Convert.ToBase64String(data);

                    DateTime date = DateTime.Now;
                    string nString = "Pic" + date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Second.ToString() + date.Millisecond.ToString() + ".jpg";
                    var guardarImagenStatus = DependencyService.Get<IFileService>().SavePicture(nString, mStream);
                    if (guardarImagenStatus == true)
                    {

                        var documentsPath = DependencyService.Get<IFileService>().GetPicturePath();
                        string filePath = Path.Combine(documentsPath, nString);

                        Guardar_Datos(filePath);                       
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "No se pudo procesar la acción, asegurese de activar TODOS los permisos necesarios en este dispositivo.", "ok");
                    }
                }
                else
                {
                    await DisplayAlert("Aviso", "Ingrese los datos faltantes en los espacios vacíos.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }

        public async void PermissionStorageWrite()
        {
            var status = await CheckAndRequestPermissionAsync(new Permissions.StorageWrite());
            if (status != PermissionStatus.Granted)
            {
                // Notify user permission was denied
                return;
            }

        }

        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }

        public async void Guardar_Datos(string filepath)
        {
            var firma = new Models.Firmas
            {
                base_64 = base64Val,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text
            };

            var resultado = await App.BaseDatos.GrabarFirmas(firma);

            if (resultado == 1)
            {
                await DisplayAlert("Firma guardada", "Se ha registrado la firma exitosamente en la base de datos y en el dispositivo en la ruta: " + filepath, "ok");
                txtDescripcion.Text = txtNombre.Text = base64Val = String.Empty;
                signature.Clear();
            }
            else
            {
                await DisplayAlert("Error", "No se pudieron guardar el contenido en la Base de Datos. Inténtelo de nuevo.", "ok");
            }
        }

        public static async Task WriteStreamAsync(string path, Stream contents)
        {
            using (var writer = File.Create(path))
            {
                await contents.CopyToAsync(writer).ConfigureAwait(false);
            }
        }

        private async void btnlist_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }
    }
}