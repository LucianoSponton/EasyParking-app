using EasyParking.Views.Estacionamientos;
using EasyParking.Views.Generales;
using EasyParking.Views.Reservas.Reserva;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using ServiceWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Favoritos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favoritos : ContentPage
    {
        FavoritoServiceWebApi favoritoServiceWebApi = new FavoritoServiceWebApi(App.WebApiAccess);

        public Favoritos()
        {
            InitializeComponent();
            CargarResultados();
        }

        async Task CargarResultados() // 27,4552894 58,9860366
        {
            Geocoder geoCoder = new Geocoder();
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));    //
            var cts = new CancellationTokenSource();                                                     // obtengo mi posicion actual
            var posicion_actual = await Geolocation.GetLocationAsync(request, cts.Token);                //

            activityIndicator.IsVisible = true;
            lwlistado.ItemsSource = null;
            var Lista = await favoritoServiceWebApi.GetMisFavoritos();

            foreach (var item in Lista)
            {
                //ServiceWebApi.DTO.EstacionamientoDTO estacionamientoDTO = new ServiceWebApi.DTO.EstacionamientoDTO();
                //estacionamientoDTO = Tools.Tools.PropertyCopier<Model.Estacionamiento, ServiceWebApi.DTO.EstacionamientoDTO>.Copy(item,estacionamientoDTO);

                item.Nombre = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.Nombre.ToLower());
                item.Direccion = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item.Direccion.ToLower());


                IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(item.Direccion + "resistencia chaco");   //
                Position position = approximateLocations.FirstOrDefault();                                                              // obtengo la posicion del lugar basado en la direccion              
                string coordinates = $"{position.Latitude}, {position.Longitude}";                                                      //

                Location posicion_ultima = new Location();
                posicion_ultima.Latitude = position.Latitude;
                posicion_ultima.Longitude = position.Longitude;

                //await Xamarin.Essentials.Map.OpenAsync(position.Latitude, position.Longitude);

                double distancia = Location.CalculateDistance(posicion_actual, posicion_ultima, DistanceUnits.Kilometers); // resultado en kilometros de la distancia entre las dos posiciones

                item.Distancia = distancia;

            }

            lwlistado.ItemsSource = Lista.OrderBy(x => x.Distancia);
            activityIndicator.IsVisible = false;

            if (Lista.Count == 0)
            {
                BoxNotSearchResult.IsVisible = true;
            }
            else
            {
                BoxNotSearchResult.IsVisible = false;
            }
        }


        private async void Card_Parking_OnClicked_Reserva(object sender, EventArgs e)
        {
            var detalle = Tools.Tools.DarFormaAObjeto(sender);

            await PopupNavigation.Instance.PushAsync(new PopupReserva(detalle));
        }

        private async void Card_Parking_OnClicked_VerMas(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopCargando());
            var detalle = Tools.Tools.DarFormaAObjeto(sender);
            await Navigation.PushAsync(new DetalleDeEstacionamiento(detalle));
            await PopupNavigation.Instance.PopAsync();

        }

        private async void Card_Parking_OnClicked_VerMapa(object sender, EventArgs e)
        {
            var detalle = Tools.Tools.DarFormaAObjeto(sender);
            string direccion = detalle.Direccion.Trim();
            direccion = direccion.Replace(" ", "+");
            await Launcher.OpenAsync($"geo:0,0?q={direccion}+Resistencia+Chaco");
        }

        private async void Card_Parking_OnClicked_VeImagen(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ContentView;
                var detalle = btn?.BindingContext as Model.Estacionamiento;
                var p = new PopupItemImage(detalle.Imagen);
                await PopupNavigation.Instance.PushAsync(p);
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        private async void Card_Parking_OnClicked_Favorito(object sender, EventArgs e)
        {
            var btn = sender as ContentView;
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
    }
}