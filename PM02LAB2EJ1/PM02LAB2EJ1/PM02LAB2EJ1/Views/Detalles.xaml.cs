using PM02LAB2EJ1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02LAB2EJ1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detalles : ContentPage
    {
        public Detalles(Firmas firmas)
        {
            InitializeComponent();
            lblnombre.Text = "Nombre: " + firmas.nombre;
            lbldescripcion.Text = "Descripción: " + firmas.descripcion;
            imgSignature.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(firmas.base_64)));

        }
    }
}