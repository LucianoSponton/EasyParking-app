﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EasyParking.Views.Estacionamientos.MisEstacionamientos;
using EasyParking.Views.PerfilDeNegocio.Tarifas.Tarifa;
using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using ServiceWebApi.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Graphics;

namespace EasyParking.Views.Estacionamientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarEstacionamiento : ContentPage
    {
        List<byte[]> BytesLista = new List<byte[]>();
        byte[] ImagenArray = null;
        string Modalidad;
        EstacionamientoServiceWebApi estacionamientoServiceWebApi;

        private Model.Estacionamiento _estacionamiento;

        private int _estacionamientoId;


        Stream STREAM;
        List<ImageSource> ImageSourceLista = new List<ImageSource>();

        public ImageSource EstacionamientoImagen { get; set; }


        public EditarEstacionamiento(int estacionamientoId)
        {
            InitializeComponent();

            imagenEmpty.IsVisible = labelimagenEmpty.IsVisible = false;

            _estacionamientoId = estacionamientoId;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);
                _estacionamiento = await estacionamientoServiceWebApi.Get(_estacionamientoId);
                CargarDatosParaEditar();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }


        void CargarDatosParaEditar()
        {
            ImagenArray = _estacionamiento.Imagen;

            Imagen.Source = ImageSource.FromStream(() =>                //
            {                                                                   // Convierte de array de bytes a source imagen
                return new MemoryStream(_estacionamiento.Imagen);             //
            });

            comboboxCiudad.Text = _estacionamiento.Ciudad; // CIUDAD DEL LUGAR

            entryNombre.Text = _estacionamiento.Nombre; // NOMBRE DEL LUGAR

            entryDireccion.Text = _estacionamiento.Direccion; // DIRECCION DEL LUGAR

            comboBoxTipoDeLugar.Text = _estacionamiento.TipoDeLugar; // TIPO DEL LUGAR

            foreach (var item in _estacionamiento.Jornadas)
            {

                switch (item.DiaDeLaSemana)
                {
                    case Model.Enums.Dia.LUNES:
                        lwHorariosLunes.ItemsSource = item.Horarios;
                        break;
                    case Model.Enums.Dia.MARTES:
                        lwHorariosMartes.ItemsSource = item.Horarios;
                        break;
                    case Model.Enums.Dia.MIERCOLES:
                        lwHorariosMiercoles.ItemsSource = item.Horarios;
                        break;
                    case Model.Enums.Dia.JUEVES:
                        lwHorariosJueves.ItemsSource = item.Horarios;
                        break;
                    case Model.Enums.Dia.VIERNES:
                        lwHorariosViernes.ItemsSource = item.Horarios;
                        break;
                    case Model.Enums.Dia.SABADO:
                        lwHorariosSabado.ItemsSource = item.Horarios;
                        break;
                    case Model.Enums.Dia.DOMINGO:
                        lwHorariosDomingo.ItemsSource = item.Horarios;
                        break;
                }

            }
            //********** TEMA VEHICULOS ACEPTADOS Y SUS TARIFAS **********//

            foreach (var item in _estacionamiento.TiposDeVehiculosAdmitidos)
            {
                if (item.TipoDeVehiculo.ToLower() == "auto")
                {
                    checkBoxAuto.IsChecked = true;
                    frameTarifaAuto.IsVisible = true;
                    entryCantidadAuto.Value = item.CapacidadDeAlojamiento;
                    entryAuto_TarifaHora.Text = item.Tarifa_Hora.ToString();
                    entryAuto_TarifaDia.Text = item.Tarifa_Dia.ToString();
                    entryAuto_TarifaSemana.Text = item.Tarifa_Semana.ToString();
                    entryAuto_TarifaMes.Text = item.Tarifa_Mes.ToString();
                }
                if (item.TipoDeVehiculo.ToLower() == "moto")
                {
                    checkBoxMoto.IsChecked = true;
                    frameTarifaMoto.IsVisible = true;
                    entryCantidadMoto.Value = item.CapacidadDeAlojamiento;
                    entryMoto_TarifaHora.Text = item.Tarifa_Hora.ToString();
                    entryMoto_TarifaDia.Text = item.Tarifa_Dia.ToString();
                    entryMoto_TarifaSemana.Text = item.Tarifa_Semana.ToString();
                    entryMoto_TarifaMes.Text = item.Tarifa_Mes.ToString();
                }
                if (item.TipoDeVehiculo.ToLower() == "camioneta")
                {
                    checkBoxCamioneta.IsChecked = true;
                    frameTarifaCamioneta.IsVisible = true;
                    entryCantidadCamioneta.Value = item.CapacidadDeAlojamiento;
                    entryCamioneta_TarifaHora.Text = item.Tarifa_Hora.ToString();
                    entryCamioneta_TarifaDia.Text = item.Tarifa_Dia.ToString();
                    entryCamioneta_TarifaSemana.Text = item.Tarifa_Semana.ToString();
                    entryCamioneta_TarifaMes.Text = item.Tarifa_Mes.ToString();
                }
            }

            entryMontoReserva.Text = _estacionamiento.MontoReserva.ToString(); // MONTO DE LA RESERVA

        }
        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                STREAM = await result.OpenReadAsync();

                Imagen.Source = ImageSource.FromStream(() => STREAM);
                Imagen.Rotation = 90;
                imagenEmpty.IsVisible = labelimagenEmpty.IsVisible = false;
                ImageSource IM = Imagen.Source;
                ConvertSourceToBytes4(result, IM);
                //STREAM.Dispose();
            }



        }


        private async void ConvertSourceToBytes4(FileResult result, ImageSource IM)
        {


            if (Imagen.Source != null)
            {

                StreamImageSource streamImageSource = (StreamImageSource)ImageSource.FromStream(() => STREAM);
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                STREAM = await result.OpenReadAsync();
                Imagen.Source = ImageSource.FromStream(() => STREAM);
                imagenEmpty.IsVisible = labelimagenEmpty.IsVisible = false;

                byte[] imageArray = null;

                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    imageArray = memory.ToArray();
                }

                ImagenArray = imageArray;
            }
        }

        private async void ConvertSourceToBytes4(MediaFile result, ImageSource IM)
        {


            if (Imagen.Source != null)
            {

                StreamImageSource streamImageSource = (StreamImageSource)IM;
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                var stream2 = result.GetStream();
                Imagen.Source = ImageSource.FromStream(() => stream2);
                imagenEmpty.IsVisible = labelimagenEmpty.IsVisible = false;

                byte[] imageArray = null;

                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    imageArray = memory.ToArray();
                }

                ImagenArray = imageArray;
            }
        }



        private async void btnSeleccionarFoto_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Seleccion de fotos NO permitida", "Se require permisos para seleccionar fotos.", "Entendido");
                    return;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,

                });


                if (file == null)
                {
                    return;

                }
                else
                {
                    imagenEmpty.IsVisible = labelimagenEmpty.IsVisible = false;

                }

                Imagen.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                ConvertSourceToBytes4(file, Imagen.Source);

                //file.Dispose();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }


        private async void btnEditarHorario_Clicked(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            var Dia = btn.ClassId;
            Model.Enums.Dia dia = (Model.Enums.Dia)System.Enum.Parse(typeof(Model.Enums.Dia), Dia);

            var p = new PopupRangoHorario();
            await PopupNavigation.Instance.PushAsync(p);
            p.MyEvent += async (args, args2) =>
            {
                List<string> respuesta = args; // lista de los horarios en string
                List<RangoH> ListaDeHorarios = args2; // lista de los horarios en objeto RangoH

                if (respuesta.Count != 0)
                {
                    switch (dia)
                    {
                        case Model.Enums.Dia.LUNES:
                            //_estacionamiento.Jornadas[0].Horarios = ListaDeHorarios;
                            // _estacionamiento.Jornadas[0].DiaDeLaSemana = dia;
                            lwHorariosLunes.ItemsSource = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.MARTES:
                            //_estacionamiento.Jornadas[1].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[1].DiaDeLaSemana = dia;
                            lwHorariosMartes.ItemsSource = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.MIERCOLES:
                            //_estacionamiento.Jornadas[2].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[2].DiaDeLaSemana = dia;
                            lwHorariosMiercoles.ItemsSource = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.JUEVES:
                            //_estacionamiento.Jornadas[3].Horarios = ListaDeHorarios;
                            ///_estacionamiento.Jornadas[3].DiaDeLaSemana = dia;
                            lwHorariosJueves.ItemsSource = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.VIERNES:
                            //_estacionamiento.Jornadas[4].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[4].DiaDeLaSemana = dia;
                            lwHorariosViernes.ItemsSource = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.SABADO:
                            //_estacionamiento.Jornadas[5].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[5].DiaDeLaSemana = dia;
                            lwHorariosSabado.ItemsSource = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.DOMINGO:
                            //_estacionamiento.Jornadas[6].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[6].DiaDeLaSemana = dia;
                            lwHorariosDomingo.ItemsSource = ListaDeHorarios;
                            break;
                    }

                }
            };
        }




        private void checkBoxAuto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((bool)checkBoxAuto.IsChecked)
            {
                frameTarifaAuto.IsVisible = entryCantidadAuto.IsEnabled = true;
            }
            else
            {
                frameTarifaAuto.IsVisible = entryCantidadAuto.IsEnabled = false;

            }
        }

        private void checkBoxCamioneta_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((bool)checkBoxCamioneta.IsChecked)
            {
                frameTarifaCamioneta.IsVisible = entryCantidadCamioneta.IsEnabled = true;
            }
            else
            {
                frameTarifaCamioneta.IsVisible = entryCantidadCamioneta.IsEnabled = false;
            }
        }

        private void checkBoxMoto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((bool)checkBoxMoto.IsChecked)
            {
                frameTarifaMoto.IsVisible = entryCantidadMoto.IsEnabled = true;
            }
            else
            {
                frameTarifaMoto.IsVisible = entryCantidadMoto.IsEnabled = false;
            }
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                activityIndicadorBtnGuardar.IsEnabled = false;
                activityIndicadorBtnGuardar.IsVisible = activityIndicadorBtnGuardar.IsRunning = true;

                if (_estacionamiento.Imagen != null)
                {
                    _estacionamiento.Imagen = ImagenArray; // IMAGEN DEL LUGAR 
                }
                else
                {
                    await DisplayAlert("Error", "Deber ingresar alguna imagen o foto del lugar", "Entendido");
                    return;
                }

                _estacionamiento.Ciudad = comboboxCiudad.Text; // CIUDAD DEL LUGAR

                if (!string.IsNullOrEmpty(entryNombre.Text))
                {
                    _estacionamiento.Nombre = entryNombre.Text; // NOMBRE DEL LUGAR

                }
                else
                {
                    await DisplayAlert("Error", "Deber ingresar el nombre del lugar", "Entendido");
                    return;
                }

                if (true)
                {
                    _estacionamiento.Direccion = entryDireccion.Text; // DIRECCION DEL LUGAR

                }
                else
                {
                    await DisplayAlert("Error", "Deber ingresar la direccion del lugar", "Entendido");
                    return;
                }
                _estacionamiento.TipoDeLugar = comboBoxTipoDeLugar.Text; // TIPO DEL LUGAR

                // LOS RANGO HORARIOS YA SE CARGARON ANTES EN EL EVENTO --> btnEditarHorario_Clicked
                if (_estacionamiento.TiposDeVehiculosAdmitidos.Any(x => x.TipoDeVehiculo.ToLower() == "auto")) // SI YA EXISTE UN AUTO MODIFICO SOBRE ESE
                {
                    if ((bool)checkBoxAuto.IsChecked && Convert.ToInt32(entryCantidadAuto.Value) != 0)  // AUTO DESDE ACA
                    {

                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "auto").FirstOrDefault().CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadAuto.Value);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "auto").FirstOrDefault().Tarifa_Hora = Convert.ToDecimal(entryAuto_TarifaHora.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "auto").FirstOrDefault().Tarifa_Dia = Convert.ToDecimal(entryAuto_TarifaDia.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "auto").FirstOrDefault().Tarifa_Semana = Convert.ToDecimal(entryAuto_TarifaSemana.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "auto").FirstOrDefault().Tarifa_Mes = Convert.ToDecimal(entryAuto_TarifaMes.Text);
                    }
                    else if ((bool)checkBoxAuto.IsChecked && Convert.ToInt32(entryCantidadAuto.Value) == 0)
                    {
                        await DisplayAlert("Error", "Ha seleccionado Auto como vehiculo a alojar, debe indicar la cantidad de vehiculos de ese tipo que admite en el lugar", "Entendido");
                        return;
                    }
                }

                if (_estacionamiento.TiposDeVehiculosAdmitidos.Any(x => x.TipoDeVehiculo.ToLower() == "camioneta")) // SI YA EXISTE UNA CVAMIONETA MODIFICO SOBRE ESE
                {
                    if ((bool)checkBoxCamioneta.IsChecked && Convert.ToInt32(entryCantidadCamioneta.Value) != 0)  // CAMIONETA DESDE ACA
                    {
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "camioneta").FirstOrDefault().CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadCamioneta.Value);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "camioneta").FirstOrDefault().Tarifa_Hora = Convert.ToDecimal(entryCamioneta_TarifaHora.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "camioneta").FirstOrDefault().Tarifa_Dia = Convert.ToDecimal(entryCamioneta_TarifaDia.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "camioneta").FirstOrDefault().Tarifa_Semana = Convert.ToDecimal(entryCamioneta_TarifaSemana.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "camioneta").FirstOrDefault().Tarifa_Mes = Convert.ToDecimal(entryCamioneta_TarifaMes.Text);
                    }
                    else if ((bool)checkBoxCamioneta.IsChecked && Convert.ToInt32(entryCantidadCamioneta.Value) == 0)
                    {
                        await DisplayAlert("Error", "Ha seleccionado Camioneta como vehiculo a alojar, debe indicar la cantidad de vehiculos de ese tipo que admite en el lugar", "Entendido");
                        return;
                    }
                }

                if (_estacionamiento.TiposDeVehiculosAdmitidos.Any(x => x.TipoDeVehiculo.ToLower() == "moto")) // SI YA EXISTE UN MOTO MODIFICO SOBRE ESE
                {
                    if ((bool)checkBoxMoto.IsChecked && Convert.ToInt32(entryCantidadMoto.Value) != 0) // MOTO DESDE ACA
                    {
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "moto").FirstOrDefault().CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadMoto.Value);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "moto").FirstOrDefault().Tarifa_Hora = Convert.ToDecimal(entryMoto_TarifaHora.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "moto").FirstOrDefault().Tarifa_Dia = Convert.ToDecimal(entryMoto_TarifaDia.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "moto").FirstOrDefault().Tarifa_Semana = Convert.ToDecimal(entryMoto_TarifaSemana.Text);
                        _estacionamiento.TiposDeVehiculosAdmitidos.Where(x => x.TipoDeVehiculo.ToLower() == "moto").FirstOrDefault().Tarifa_Mes = Convert.ToDecimal(entryMoto_TarifaMes.Text);

                    }
                    else if ((bool)checkBoxMoto.IsChecked && Convert.ToInt32(entryCantidadMoto.Value) == 0)
                    {
                        await DisplayAlert("Error", "Ha seleccionado Moto como vehiculo a alojar, debe indicar la cantidad de vehiculos de ese tipo que admite en el lugar", "Entendido");
                        return;
                    }
                }



               

                if ((bool)checkBoxMoto.IsChecked == false && (bool)checkBoxAuto.IsChecked == false && (bool)checkBoxCamioneta.IsChecked == false)
                {
                    await DisplayAlert("Error", "Debe seleccionar al menos 1 tipo de vehiculo que admite en el lugar y sus respectivas tarifas y capacidad de dicho vehiculo", "Entendido");
                    return;
                }

                //********************//

                _estacionamiento.MontoReserva = Convert.ToDecimal(entryMontoReserva.Text); // MONTO DE LA RESERVA

                var xxx = _estacionamiento;

                await estacionamientoServiceWebApi.Update(_estacionamiento);
                Tools.Tools.Messages("se guardaron los cambios");
                await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
            finally
            {
                activityIndicadorBtnGuardar.IsEnabled = true;
                activityIndicadorBtnGuardar.IsVisible = activityIndicadorBtnGuardar.IsRunning = false;
            }

        }
    }
}