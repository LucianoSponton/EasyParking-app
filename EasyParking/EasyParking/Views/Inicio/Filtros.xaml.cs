using Syncfusion.XForms.Buttons;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Filtros : ContentPage
    {
        private ServiceWebApi.DTO.ParametroBusquedaDTO _parametroBusquedaDTO = new ServiceWebApi.DTO.ParametroBusquedaDTO();
        public Filtros(ServiceWebApi.DTO.ParametroBusquedaDTO parametroBusquedaDTO)
        {
            InitializeComponent();
            //_parametroBusquedaDTO = new ServiceWebApi.DTO.ParametroBusquedaDTO();
            _parametroBusquedaDTO = parametroBusquedaDTO;

            CargarParametrosDeBusqueda();
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

                await DisplayAlert("Error ...", Tools.Tools.TraduceError(ex), "Entendido");
            }

            await Navigation.PopAsync();


        }

        //protected override bool OnBackButtonPressed()
        //{
        //    return base.OnBackButtonPressed();
        //}


        void CargarParametrosDeBusqueda()
        {
            if (_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("Auto")))
            {
                chipAuto.BackgroundColor = Color.FromHex("FFCC3D");
                chipAuto.IsChecked = true;
                chipAuto.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("Camioneta")))
            {
                chipCamioneta.BackgroundColor = Color.FromHex("FFCC3D");
                chipCamioneta.IsChecked = true;
                chipCamioneta.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("Moto")))
            {
                chipMoto.BackgroundColor = Color.FromHex("FFCC3D");
                chipMoto.IsChecked = true;
                chipMoto.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("Todos los vehiculos")))
            {
                chipVehiculosTodos.BackgroundColor = Color.FromHex("FFCC3D");
                chipVehiculosTodos.IsChecked = true;
                chipVehiculosTodos.ShowCloseButton = true;
                chipAuto.IsChecked = false;
                chipAuto.ShowCloseButton = false;
                chipMoto.IsChecked = false;
                chipMoto.ShowCloseButton = false;
                chipCamioneta.IsChecked = false;
                chipCamioneta.ShowCloseButton = false;

                _parametroBusquedaDTO.TipoDeVehiculos.Clear();
                _parametroBusquedaDTO.TipoDeVehiculos.Add("Todos los vehiculos");
            }

            //////////////// *********************** //////////////////

            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Terreno al aire libre")))
            {
                chipTerrenoAlAireLibre.BackgroundColor = Color.FromHex("FFCC3D");
                chipTerrenoAlAireLibre.IsChecked = true;
                chipTerrenoAlAireLibre.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Terreno parialmente cubierto")))
            {
                chipTerrenoParcialmenteCubierto.BackgroundColor = Color.FromHex("FFCC3D");
                chipTerrenoParcialmenteCubierto.IsChecked = true;
                chipTerrenoParcialmenteCubierto.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Galpón abierto")))
            {
                chipGalponAbierto.BackgroundColor = Color.FromHex("FFCC3D");
                chipGalponAbierto.IsChecked = true;
                chipGalponAbierto.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Galpón cerrado")))
            {
                chipGalponCerrado.BackgroundColor = Color.FromHex("FFCC3D");
                chipGalponCerrado.IsChecked = true;
                chipGalponCerrado.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Lugar bajo edificio")))
            {
                chipLugarBajoEdificio.BackgroundColor = Color.FromHex("FFCC3D");
                chipLugarBajoEdificio.IsChecked = true;
                chipLugarBajoEdificio.ShowCloseButton = true;
            }


            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("casa")))
            {
                chipCasa.BackgroundColor = Color.FromHex("FFCC3D");
                chipCasa.IsChecked = true;
                chipCasa.ShowCloseButton = true;
            }

            if (_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Todos los lugares")))
            {
                chipTipoDeLugarTodos.BackgroundColor = Color.FromHex("FFCC3D");
                chipTipoDeLugarTodos.IsChecked = true;
                chipTipoDeLugarTodos.ShowCloseButton = true;

                chipVehiculosTodos.BackgroundColor = Color.FromHex("FFCC3D");
                chipVehiculosTodos.IsChecked = true;
                chipVehiculosTodos.ShowCloseButton = true;

                chipLugarBajoEdificio.IsChecked = false;
                chipLugarBajoEdificio.ShowCloseButton = false;
                chipGalponCerrado.IsChecked = false;
                chipGalponCerrado.ShowCloseButton = false;
                chipGalponAbierto.IsChecked = false;
                chipGalponAbierto.ShowCloseButton = false;
                chipTerrenoAlAireLibre.IsChecked = false;
                chipTerrenoAlAireLibre.ShowCloseButton = false;
                chipTerrenoParcialmenteCubierto.IsChecked = false;
                chipTerrenoParcialmenteCubierto.ShowCloseButton = false;
                chipCasa.IsChecked = false;
                chipCasa.ShowCloseButton = false;
            }


        }



        private void chip_Clicked(object sender, EventArgs e)
        {
            switch ((sender as SfChip).ClassId)
            {
                case "chipAuto":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipAuto.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipAuto.IsChecked = chipAuto.ShowCloseButton = false;
                    }
                    else
                    {
                        chipAuto.BackgroundColor = Color.FromHex("FFCC3D");
                        chipAuto.IsChecked = true;
                        chipAuto.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("auto")))
                        {
                            _parametroBusquedaDTO.TipoDeVehiculos.Add("auto");
                        }
                    }
                    break;
                case "chipCamioneta":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipCamioneta.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipCamioneta.IsChecked = chipCamioneta.ShowCloseButton = false;
                    }
                    else
                    {
                        chipCamioneta.BackgroundColor = Color.FromHex("FFCC3D");
                        chipCamioneta.IsChecked = true;
                        chipCamioneta.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("camioneta")))
                        {
                            _parametroBusquedaDTO.TipoDeVehiculos.Add("camioneta");
                        }
                    }
                    break;
                case "chipMoto":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipMoto.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipMoto.IsChecked = chipMoto.ShowCloseButton = false;
                    }
                    else
                    {
                        chipMoto.BackgroundColor = Color.FromHex("FFCC3D");
                        chipMoto.IsChecked = true;
                        chipMoto.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("moto")))
                        {
                            _parametroBusquedaDTO.TipoDeVehiculos.Add("moto");
                        }
                    }
                    break;
                case "chipVehiculosTodos":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipVehiculosTodos.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipVehiculosTodos.IsChecked = chipVehiculosTodos.ShowCloseButton = false;
                    }
                    else
                    {
                        chipVehiculosTodos.BackgroundColor = Color.FromHex("FFCC3D");
                        chipVehiculosTodos.ShowCloseButton = true;
                        chipVehiculosTodos.IsChecked = true;

                        if (!_parametroBusquedaDTO.TipoDeVehiculos.Exists(x => x.Equals("Todos los vehiculos")))
                        {
                            _parametroBusquedaDTO.TipoDeVehiculos.Clear();
                            _parametroBusquedaDTO.TipoDeVehiculos.Add("Todos los vehiculos");
                        }
                        chipAuto.IsChecked = chipMoto.IsChecked = chipCamioneta.IsChecked = false;

                        chipAuto.IsChecked = false;
                        chipAuto.ShowCloseButton = false;
                        chipMoto.IsChecked = false;
                        chipMoto.ShowCloseButton = false;
                        chipCamioneta.IsChecked = false;
                        chipCamioneta.ShowCloseButton = false;

                        chipCamioneta.BackgroundColor = chipMoto.BackgroundColor = chipAuto.BackgroundColor = Color.FromHex("#AEB6BF");


                    }
                    break;
                case "chipTerrenoAlAireLibre":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipTerrenoAlAireLibre.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipTerrenoAlAireLibre.IsChecked = chipTerrenoAlAireLibre.ShowCloseButton = false;
                    }
                    else
                    {
                        chipTerrenoAlAireLibre.BackgroundColor = Color.FromHex("FFCC3D");
                        chipTerrenoAlAireLibre.IsChecked = chipTerrenoAlAireLibre.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Terreno al aire libre")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Terreno al aire libre");
                        }

                    }
                    break;
                case "chipTerrenoParcialmenteCubierto":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipTerrenoParcialmenteCubierto.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipTerrenoParcialmenteCubierto.IsChecked = chipTerrenoParcialmenteCubierto.ShowCloseButton = false;
                    }
                    else
                    {
                        chipTerrenoParcialmenteCubierto.BackgroundColor = Color.FromHex("FFCC3D");
                        chipTerrenoParcialmenteCubierto.IsChecked = chipTerrenoParcialmenteCubierto.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Terreno parcialmente cubierto")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Terreno parcialmente cubierto");
                        }

                    }
                    break;
                case "chipGalponAbierto":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipGalponAbierto.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipGalponAbierto.IsChecked = chipGalponAbierto.ShowCloseButton = false;
                    }
                    else
                    {
                        chipGalponAbierto.BackgroundColor = Color.FromHex("FFCC3D");
                        chipGalponAbierto.IsChecked = chipGalponAbierto.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Galpon abierto")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Galpon abierto");
                        }

                    }
                    break;
                case "chipGalponCerrado":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipGalponCerrado.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipGalponCerrado.IsChecked = chipGalponCerrado.ShowCloseButton = false;
                    }
                    else
                    {
                        chipGalponCerrado.BackgroundColor = Color.FromHex("FFCC3D");
                        chipGalponCerrado.IsChecked = chipGalponCerrado.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Galpon cerrado")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Galpon cerrado");
                        }

                    }
                    break;
                case "chipLugarBajoEdificio":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipLugarBajoEdificio.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipLugarBajoEdificio.IsChecked = chipLugarBajoEdificio.ShowCloseButton = false;
                    }
                    else
                    {
                        chipLugarBajoEdificio.BackgroundColor = Color.FromHex("FFCC3D");
                        chipLugarBajoEdificio.IsChecked = chipLugarBajoEdificio.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Lugar bajo edificio")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Lugar bajo edificio");
                        }

                    }
                    break;
                case "chipCasa":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipCasa.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipCasa.IsChecked = chipCasa.ShowCloseButton = false;
                    }
                    else
                    {
                        chipCasa.BackgroundColor = Color.FromHex("FFCC3D");
                        chipCasa.IsChecked = chipCasa.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Casa")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Casa");
                        }

                    }
                    break;
                case "chipTipoDeLugarTodos":
                    if ((sender as SfChip).IsChecked)
                    {
                        chipTipoDeLugarTodos.BackgroundColor = Color.FromHex("#AEB6BF");
                        chipTipoDeLugarTodos.IsChecked = chipTipoDeLugarTodos.ShowCloseButton = false;
                    }
                    else
                    {
                        chipTipoDeLugarTodos.BackgroundColor = Color.FromHex("FFCC3D");
                        chipTipoDeLugarTodos.IsChecked = chipTipoDeLugarTodos.ShowCloseButton = true;

                        if (!_parametroBusquedaDTO.TipoDeLugars.Exists(x => x.Equals("Todos los lugares")))
                        {
                            _parametroBusquedaDTO.TipoDeLugars.Add("Todos los lugares");
                        }

                        chipCasa.IsChecked = chipLugarBajoEdificio.IsChecked = chipGalponCerrado.IsChecked = chipGalponAbierto.IsChecked = chipTerrenoAlAireLibre.IsChecked = chipTerrenoParcialmenteCubierto.IsChecked = false;
                        chipCasa.ShowCloseButton = chipLugarBajoEdificio.ShowCloseButton = chipGalponCerrado.ShowCloseButton = chipGalponAbierto.ShowCloseButton = chipTerrenoAlAireLibre.ShowCloseButton = chipTerrenoParcialmenteCubierto.ShowCloseButton = false;
                        chipCasa.BackgroundColor = chipLugarBajoEdificio.BackgroundColor = chipGalponCerrado.BackgroundColor = chipGalponAbierto.BackgroundColor = chipTerrenoAlAireLibre.BackgroundColor = chipTerrenoParcialmenteCubierto.BackgroundColor = Color.FromHex("#AEB6BF");
                    }
                    break;
                default:
                    break;
            }

        }

        private async void btnAplicar_Clicked(object sender, EventArgs e)
        {
            activityIndicadorBtnAplicar.IsEnabled = false;
            activityIndicadorBtnAplicar.IsVisible = activityIndicadorBtnAplicar.IsRunning = true;

            App.parametroBusquedaDTO = _parametroBusquedaDTO;

            await Task.Delay(200);

            await CloseAllPopup();
        }
    }
}