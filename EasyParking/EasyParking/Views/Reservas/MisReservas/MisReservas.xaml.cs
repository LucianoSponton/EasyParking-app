using EasyParking.Views.Reseñas;
using ServiceWebApi;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Reservas.MisReservas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisReservas : ContentPage
    {
        ReservaServiceWebApi reservaServiceWebApi = new ReservaServiceWebApi(App.WebApiAccess);
        public MisReservas()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                activityIndicator.IsVisible = true;

                var lista = await reservaServiceWebApi.GetMisReservas();
                var lista2 = await reservaServiceWebApi.GetAll();

                lwlistado.ItemsSource = lista;

                activityIndicator.IsVisible = false;

                if (lista.Count == 0)
                {
                    BoxNotSearchResult.IsVisible = true;
                }
                else
                {
                    BoxNotSearchResult.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        private async void btnCancelarReserva_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Cancelar Reserva", "¿seguro desea cancelarla?", "Si, cancelar", "No");

            if (result)
            {
                Tools.Tools.Messages("Reserva eliminada");
            }
        }

        private async void btnAgregarReseña_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarReseña());
        }
    }
}