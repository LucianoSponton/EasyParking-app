using ServiceWebApi;
using ServiceWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Estacionamientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDeEstacionamiento : ContentPage
    {
        EstacionamientoServiceWebApi estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);
        FavoritoServiceWebApi favoritoServiceWebApi = new FavoritoServiceWebApi(App.WebApiAccess);

        private Model.Estacionamiento _estacionamiento;
        public DetalleDeEstacionamiento(ServiceWebApi.DTO.EstacionamientoDTO estacionamiento)
        {
            InitializeComponent();
            estacionamiento.TiposDeVehiculosAdmitidos.OrderBy(x => x.TipoDeVehiculo);
            _estacionamiento = estacionamiento;
            BindingContext = estacionamiento;

            List<string> lista = new List<string>();

            foreach (var item in estacionamiento.TiposDeVehiculosAdmitidos.OrderBy(x => x.TipoDeVehiculo))
            {
                lista.Add(item.TipoDeVehiculo);
            }

            comboBox.DataSource = lista;

            comboBox.Text = lista[0];
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            lwJornadas.ItemsSource = await estacionamientoServiceWebApi.GetJornadas(_estacionamiento.Id);
        }

        private async void btnReserva_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new WebViewVideos());
            await Browser.OpenAsync("https://mpago.la/2as9Da5");
        }

        private async void btnVerReseñas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reseñas.Reseñas());
        }

        private void imagenCard_Clicked(object sender, EventArgs e)
        {

        }



        private void comboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            try
            {
                labelTarifa_hora.Text = _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo == comboBox.Text).FirstOrDefault().Tarifa_Hora.ToString();
                labelTarifa_dia.Text = _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo == comboBox.Text).FirstOrDefault().Tarifa_Dia.ToString();
                labelTarifa_semana.Text = _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo == comboBox.Text).FirstOrDefault().Tarifa_Semana.ToString();
                labelTarifa_mes.Text = _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo == comboBox.Text).FirstOrDefault().Tarifa_Mes.ToString();

            }
            catch (Exception ex)
            {


            }
        }

        private async void btnVerMapa_Clicked(object sender, EventArgs e)
        {
            var detalle = Tools.Tools.DarFormaAObjeto(sender);
            string direccion = detalle.Direccion.Trim();
            direccion = direccion.Replace(" ", "+");
            await Launcher.OpenAsync($"geo:0,0?q={direccion}+Resistencia+Chaco");
        }

        private async void btnFavorito_Clicked(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ImageButton;
                var detalle = btn?.BindingContext as EstacionamientoDTO;

                if (detalle.Favorito == false) // Como es falso lo tengo que agregar
                {
                    await favoritoServiceWebApi.Add(detalle.Id);
                }
                else // Como es verdadero lo tengo que eliminar
                {
                    await favoritoServiceWebApi.Delete(detalle.Id);
                }

                detalle.Favorito = !detalle.Favorito;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}