using PM02LAB2EJ1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02LAB2EJ1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        public Lista()
        {
            InitializeComponent();
            cargarListado();
        }

        public async void cargarListado()
        {
            var lista = await App.BaseDatos.ObtenerListaFirmas();
            lstfirmas.ItemsSource = lista;
        }

        private void lstfirmas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lstfirmas.SelectedItem = null;
        }

        private void lstfirmas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void VerDetalles(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Firmas model = item.BindingContext as Firmas;
            //await DisplayAlert("Pais", model.nombre  , "ok");
            await Navigation.PushAsync(new Detalles(model));
        }
    }
}