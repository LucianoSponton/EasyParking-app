using EasyParking.Views.Estacionamientos;
using EasyParking.Views.Generales;
using EasyParking.Views.Reservas.Reserva;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();


            //ahora calculo la distancia entre la posicion actual recien medida y la ultima guardada
            //Location posicion_actual = new Location(-27.4528818, -58.9786916);
            //Location posicion_ultima = new Location(-27.4552894, -58.9882253);
            //double kilometros = Location.CalculateDistance(posicion_actual, posicion_ultima, DistanceUnits.Kilometers); // resultado en kilometros
            //lwlisado.ItemsSource = null;
            //lwlisado.ItemsSource = Tools.Tools.GetEstacionamientosMock();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                activityIndicator.IsVisible = true;
                lwlisado.ItemsSource = null;
                EstacionamientoServiceWebApi estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);
                var estacionamientos = await estacionamientoServiceWebApi.GetAllInclude();
                lwlisado.ItemsSource = estacionamientos;
                activityIndicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        private async void Card_Parking_OnClicked_Reserva(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupReserva());
        }

        private async void Card_Parking_OnClicked_VerMas(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopCargando());
            await Navigation.PushAsync(new DetalleDeEstacionamiento());
            await PopupNavigation.Instance.PopAsync();

        }

        private async void Card_Parking_OnClicked_VerMapa(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("geo:0,0?q=José+Hernández+465+Resistencia+Chaco");
            //await Launcher.OpenAsync("geo:0,0?q=Av. Madariaga+278+Goya+Corrientes");
            //await Launcher.OpenAsync("geo:0,0?q=297+Luis+Agote+GoyaCorrientes");}
        }

        private async void navBar_SearchBar_Focused(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Busqueda());
        }

        private async void Card_Parking_OnClicked_VeImagen(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ContentView;
                var detalle  = btn?.BindingContext as Model.Estacionamiento; 
                var p = new PopupItemImage(detalle.Imagen);
                await PopupNavigation.Instance.PushAsync(p);
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }
    }
}