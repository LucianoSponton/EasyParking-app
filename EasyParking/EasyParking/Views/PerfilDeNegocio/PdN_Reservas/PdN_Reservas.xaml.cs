using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.PerfilDeNegocio.PdN_Reservas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdN_Reservas : ContentPage
    {
        ReservaServiceWebApi reservaServiceWebApi = new ReservaServiceWebApi(App.WebApiAccess);

        public PdN_Reservas()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                activityIndicator.IsVisible = true;

                var lista = await reservaServiceWebApi.GetReservasModalidadDueño();

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

        private async void btnHaLlegado_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupVerificacionLlegada());
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Cancelar Reserva", "¿seguro desea cancelarla?", "Si, cancelar", "No");
        }
    }
}