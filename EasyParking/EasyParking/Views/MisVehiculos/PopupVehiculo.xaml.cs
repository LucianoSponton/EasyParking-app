using Model;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.MisVehiculos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupVehiculo
    {
        VehiculoServiceWebApi vehiculoServiceWeb = new VehiculoServiceWebApi(App.WebApiAccess);

        public PopupVehiculo()
        {
            InitializeComponent();
        }

        public delegate Task MyDelegate();
        public event MyDelegate MyEvent;

        private async Task CloseAllPopup()
        {

            try
            {
                await MyEvent();
            }
            catch (Exception ex)
            {
                // Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("ERROR ...", Tools.Tools.TraduceError(ex), "Entendido");
            }

            await PopupNavigation.Instance.PopAsync();

        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.IsEnabled = false;
                if (!string.IsNullOrEmpty(entryPatente.Text) && !string.IsNullOrEmpty(comboBoxTipoDeVehiculo.Text))
                {
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.Patente = entryPatente.Text;
                    vehiculo.TipoDeVehiculo = comboBoxTipoDeVehiculo.Text;
                    await vehiculoServiceWeb.Add(vehiculo);
                    await CloseAllPopup();
                }
                else
                {
                    if (!string.IsNullOrEmpty(entryPatente.Text))
                    {
                        await DisplayAlert("Aviso", "Debe ingresar una patente", "Entendido");
                    }
                    else if (!string.IsNullOrEmpty(comboBoxTipoDeVehiculo.Text))
                    {
                        await DisplayAlert("Aviso", "Debe seleccionar el tipo de vehículo", "Entendido");//
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
            finally
            {
                btnAgregar.IsEnabled = true;
            }
        }
    }
}