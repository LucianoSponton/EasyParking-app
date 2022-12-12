using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.MisVehiculos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisVehiculos : ContentPage
    {
        VehiculoServiceWebApi vehiculoServiceWeb = new VehiculoServiceWebApi(App.WebApiAccess);
        public MisVehiculos()
        {
            InitializeComponent();

            //Vehiculo vehiculo = new Vehiculo
            //{
            //    Tipo = "Auto",
            //    Patente = "AS 654 AS"
            //};

            //Vehiculo vehiculo2 = new Vehiculo
            //{
            //    Tipo = "Moto",
            //    Patente = "AS698"
            //};

            //List<Vehiculo> lista = new List<Vehiculo>();
            //lista.Add(vehiculo);
            //lista.Add(vehiculo2);
            //lwlistado.ItemsSource = vehiculoServiceWeb.GetAll();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CargarVehiculos();
        }

        async Task CargarVehiculos()
        {
            try
            {
                activityIndicator.IsVisible = true;
                var Lista = await vehiculoServiceWeb.GetMisVehiculos();
                var Lista2 = await vehiculoServiceWeb.GetAll();

                lwlistado.ItemsSource = Lista;

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
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        class Vehiculo
        {
            public string Tipo { get; set; }
            public string Patente { get; set; }
        }


        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var p = new PopupVehiculo();
                await PopupNavigation.Instance.PushAsync(p);
                p.MyEvent += async () =>
                {
                    await CargarVehiculos();
                };
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Aviso", "¿Seguro desea eliminarlo?", "Si, eliminar", "Cancelar");

                if (result)
                {
                    var btn = sender as ImageButton;
                    var detalle = btn?.BindingContext as Model.Vehiculo;
                    await vehiculoServiceWeb.Delete(detalle.Id);
                    await CargarVehiculos();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }
    }
}