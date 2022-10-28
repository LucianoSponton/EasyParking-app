using EasyParking.Views.Generales;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Estacionamientos.MisEstacionamientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisEstacionamientos : ContentPage
    {
        EstacionamientoServiceWebApi estacionamientoServiceWebApi;
        public MisEstacionamientos()
        {
            InitializeComponent();
            estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                activityIndicator.IsVisible = true;
                lwlisado.ItemsSource = null;
                lwlisado.ItemsSource = await estacionamientoServiceWebApi.GetMisEstacionamientos();
                activityIndicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }

        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopCargando());
            await Navigation.PushAsync(new Estacionamiento());
            await PopupNavigation.Instance.PopAsync();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Eliminar Estacionamiento", "¿seguro desea eliminarlo?", "Si, eliminar", "No");

            if (result)
            {
                try
                {
                    var detalle = Tools.Tools.DarFormaAObjeto(sender);
                    await estacionamientoServiceWebApi.SetInactivo(detalle.Id);
                    Tools.Tools.Messages("Estacionamiento eliminado");
                    activityIndicator.IsVisible = true;
                    lwlisado.ItemsSource = null;
                    lwlisado.ItemsSource = await estacionamientoServiceWebApi.GetMisEstacionamientos();
                    activityIndicator.IsVisible = false;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
                }
            }
        }

        private async void btnPausar_Clicked(object sender, EventArgs e)
        {

            bool result = await DisplayAlert("Pausar publicación", "¿seguro desea pausarla?", "Si, pausar", "No");

            if (result)
            {
                try
                {
                    var detalle = Tools.Tools.DarFormaAObjeto(sender);
                    await estacionamientoServiceWebApi.SetInactivo(detalle.Id);
                    Tools.Tools.Messages("Estacionamiento eliminado");
                    activityIndicator.IsVisible = true;
                    lwlisado.ItemsSource = null;
                    lwlisado.ItemsSource = await estacionamientoServiceWebApi.GetMisEstacionamientos();
                    activityIndicator.IsVisible = false;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
                }
            }

        }

        private void imagenCard_Clicked(object sender, EventArgs e)
        {

        }
    }
}