using EasyParking.Views.MisVehiculos;
using Model;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Reservas.Reserva
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupReserva
    {
        private ServiceWebApi.DTO.EstacionamientoDTO _estacionamientoDTO;
        ReservaServiceWebApi reservaServiceWebApi = new ReservaServiceWebApi(App.WebApiAccess);
        VehiculoServiceWebApi vehiculoServiceWebApi = new VehiculoServiceWebApi(App.WebApiAccess);

        Vehiculo vehiculo;

        public PopupReserva(ServiceWebApi.DTO.EstacionamientoDTO estacionamientoDTO)
        {
            InitializeComponent();

            _estacionamientoDTO = estacionamientoDTO;

            stackEstacionamiento.BindingContext = _estacionamientoDTO;
            //radiobuttonAuto.IsChecked = true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                vehiculo = await vehiculoServiceWebApi.GetFirst();

                if (vehiculo != null)
                {
                    frameVehiculoSeleccionado.BindingContext = vehiculo;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }

        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void btnMisVehiculos_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupMisVehiculos());
        }

        private async void btnNuevoVehiculo_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupVehiculo());
        }

        private async void btnConfirmarReserva_Clicked(object sender, EventArgs e)
        {
            try
            {
                Model.Reserva reserva = new Model.Reserva();
                reserva.CodigoDeValidacion = Tools.Tools.RandomString(8);
                reserva.EstacionamientoId = _estacionamientoDTO.Id;
                reserva.Monto = _estacionamientoDTO.MontoReserva;
                reserva.Patente = vehiculo.Patente;
                reserva.VehiculoId = vehiculo.Id;
                reserva.Estado = Model.Enums.EstadoReserva.ESPERANDO_ARRIBO;
                await reservaServiceWebApi.Add(reserva);
                await PopupNavigation.Instance.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }

        }
    }
}