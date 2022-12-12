using EasyParking.Views.Estacionamientos;
using EasyParking.Views.Generales;
using EasyParking.Views.Reservas.Reserva;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using ServiceWebApi.DTO;
using Syncfusion.XForms.Buttons;
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

namespace EasyParking.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        EstacionamientoServiceWebApi estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);
        FavoritoServiceWebApi favoritoServiceWebApi = new FavoritoServiceWebApi(App.WebApiAccess);



        private readonly Geocoder _geocoder = new Geocoder();


        public Inicio()
        {
            InitializeComponent();


            //App.parametroBusquedaDTO.TipoDeVehiculos.Add("auto");

            //App.parametroBusquedaDTO.TipoDeVehiculos.Add("camioneta");

            //App.parametroBusquedaDTO.TipoDeLugars.Add("Terreno al aire libre");

            //App.parametroBusquedaDTO.TipoDeLugars.Add("casa");


            //ahora calculo la distancia entre la posicion actual recien medida y la ultima guardada
            //Location posicion_actual = new Location(-27.4528818, -58.9786916);
            //Location posicion_ultima = new Location(-27.4552894, -58.9882253);
            //double kilometros = Location.CalculateDistance(posicion_actual, posicion_ultima, DistanceUnits.Kilometers); // resultado en kilometros
            //lwlistado.ItemsSource = null;
            //lwlistado.ItemsSource = Tools.Tools.GetEstacionamientosMock();
            CargarResultados();
        }

        void CargarParametrosDeBusqueda()
        {
            lwTipoDeVehiculos.ItemsSource = null;
            lwTipoDeLugar.ItemsSource = null;

            lwTipoDeVehiculos.ItemsSource = App.parametroBusquedaDTO.TipoDeVehiculos;
            lwTipoDeLugar.ItemsSource = App.parametroBusquedaDTO.TipoDeLugars;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                //var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                //var cts = new CancellationTokenSource();
                //var posicion_actual = await Geolocation.GetLocationAsync(request, cts.Token);

                //var address = "Roque Sáenz Peña 8 resistencia chaco";

                //Geocoder geoCoder = new Geocoder();

                //IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
                //Position position = approximateLocations.FirstOrDefault();
                //string coordinates = $"{position.Latitude}, {position.Longitude}";

                //Location posicion_ultima = new Location();
                //posicion_ultima.Latitude = position.Latitude;
                //posicion_ultima.Longitude = position.Longitude;

                ////await Xamarin.Essentials.Map.OpenAsync(position.Latitude, position.Longitude);

                //double kilometros = (Location.CalculateDistance(posicion_actual, posicion_ultima, DistanceUnits.Kilometers)) * 1000; // resultado en kilometros


                if (App.parametroBusquedaDTO != null)
                {
                    CargarParametrosDeBusqueda();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        async Task CargarResultados() // 27,4552894 58,9860366
        {
            Geocoder geoCoder = new Geocoder();
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));    //
            var cts = new CancellationTokenSource();                                                     // obtengo mi posicion actual
            var posicion_actual = await Geolocation.GetLocationAsync(request, cts.Token);                //

            activityIndicator.IsVisible = true;
            lwlistado.ItemsSource = null;
            var Lista = await estacionamientoServiceWebApi.GetAllInclude();
            //lwlistado.ItemsSource = Lista;
            //activityIndicator.IsVisible = false;

            // List<ServiceWebApi.DTO.EstacionamientoDTO> listaDTO = new List<ServiceWebApi.DTO.EstacionamientoDTO>();

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
            // await Launcher.OpenAsync("geo:0,0?q=José+Hernández+465+Resistencia+Chaco");
            //await Launcher.OpenAsync("geo:0,0?q=Av. Madariaga+278+Goya+Corrientes");
            var detalle = Tools.Tools.DarFormaAObjeto(sender);
            string direccion = detalle.Direccion.Trim();
            direccion = direccion.Replace(" ", "+");
            await Launcher.OpenAsync($"geo:0,0?q={direccion}+Resistencia+Chaco");

            //var detalle = Tools.Tools.DarFormaAObjeto(sender);
            //await Xamarin.Essentials.Map.OpenAsync(detalle.Latitud, detalle.Longitud);

            // await Navigation.PushAsync(new Mapa());

        }

        private async void navBar_SearchBar_Focused(object sender, EventArgs e)
        {

            try
            {
                var p = new EasyParking.Views.Generales.Busquedas(navBar.SearchBar_Text);
                await Navigation.PushAsync(p);
                p.MyEvent += async (args) =>
                {
                    int respuesta = args;

                    await Buscar(respuesta);

                };

            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        async Task Buscar(int id)
        {
            activityIndicator.IsVisible = true;
            lwlistado.ItemsSource = null;
            var estacionamiento = await estacionamientoServiceWebApi.Get(id);
            List<Model.Estacionamiento> lista = new List<Model.Estacionamiento>();
            lista.Add(estacionamiento);
            lwlistado.ItemsSource = lista;
            activityIndicator.IsVisible = false;

            if (estacionamiento == null)
            {
                BoxNotSearchResult.IsVisible = true;
            }
            else
            {
                BoxNotSearchResult.IsVisible = false;
            }
        }

        async Task BuscarPorFiltros()
        {
            activityIndicator.IsVisible = true;
            lwlistado.ItemsSource = null;
            List<Model.Estacionamiento> lista = new List<Model.Estacionamiento>();
            lista = await estacionamientoServiceWebApi.GetConsultaGenerica(App.parametroBusquedaDTO);
            lwlistado.ItemsSource = lista;
            activityIndicator.IsVisible = false;

            if (lista == null || lista.Count == 0)
            {
                BoxNotSearchResult.IsVisible = true;
            }
            else
            {
                BoxNotSearchResult.IsVisible = false;
            }
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


        private async void pullToRefresh_Refreshing(object sender, EventArgs e)
        {
            try
            {
                pullToRefresh.IsRefreshing = true;
                // await Task.Delay(2000);
                await CargarResultados();
                pullToRefresh.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        private async void btnFiltros_Clicked(object sender, EventArgs e)
        {
            try
            {
                var p = new EasyParking.Views.Inicio.Filtros(App.parametroBusquedaDTO);

                await Navigation.PushAsync(p);

                p.MyEvent += async () =>
                {
                    await BuscarPorFiltros();
                };

            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }


        private void chipTipoDeVehiculo_Clicked(object sender, EventArgs e)
        {
            var chip = sender as SfChip;
            var item = chip?.BindingContext as String;
            List<string> lista = new List<string>(App.parametroBusquedaDTO.TipoDeVehiculos);
            lista.Remove(item);

            if (lista.Count == 0)
            {
                lwTipoDeVehiculos.IsVisible = false;
            }
            else
            {
                App.parametroBusquedaDTO.TipoDeVehiculos = lista;
                lwTipoDeVehiculos.ItemsSource = null;
                lwTipoDeVehiculos.ItemsSource = App.parametroBusquedaDTO.TipoDeVehiculos;
            }
        }

        private void chipTipoDeLugar_Clicked(object sender, EventArgs e)
        {

            var chip = sender as SfChip;
            var item = chip?.BindingContext as String;
            List<string> lista = new List<string>(App.parametroBusquedaDTO.TipoDeLugars);
            lista.Remove(item);

            if (lista.Count == 0)
            {
                lwTipoDeLugar.IsVisible = false;
            }
            else
            {
                App.parametroBusquedaDTO.TipoDeLugars = lista;
                lwTipoDeLugar.ItemsSource = null;
                lwTipoDeLugar.ItemsSource = App.parametroBusquedaDTO.TipoDeLugars;
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