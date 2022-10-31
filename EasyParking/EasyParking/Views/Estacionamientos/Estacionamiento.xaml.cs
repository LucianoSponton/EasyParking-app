using EasyParking.Views.Estacionamientos.MisEstacionamientos;
using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Estacionamientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Estacionamiento : ContentPage
    {
        List<byte[]> BytesLista = new List<byte[]>();
        byte[] ImagenArray = null;
        EstacionamientoServiceWebApi estacionamientoServiceWebApi;
        Stream STREAM;
        List<ImageSource> ImageSourceLista = new List<ImageSource>();
        Model.Estacionamiento _estacionamiento = new Model.Estacionamiento();

        List<RangoH> ListaDeHorariosLunes = new List<RangoH>();
        List<RangoH> ListaDeHorariosMartes = new List<RangoH>();
        List<RangoH> ListaDeHorariosMiercoles = new List<RangoH>();
        List<RangoH> ListaDeHorariosJueves = new List<RangoH>();
        List<RangoH> ListaDeHorariosViernes = new List<RangoH>();
        List<RangoH> ListaDeHorariosSabado = new List<RangoH>();
        List<RangoH> ListaDeHorariosDomingo = new List<RangoH>();

        public ImageSource EstacionamientoImagen { get; set; }
        public Estacionamiento()
        {
            InitializeComponent();

            estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);

            //for (int i = 0; i < 7; i++)
            //{
            //    _estacionamiento.Jornadas.Add(new Jornada());
            //}

            frameTarifaAuto.IsVisible = frameTarifaCamioneta.IsVisible = frameTarifaMoto.IsVisible = false;
            entryCantidadAuto.IsEnabled = entryCantidadCamioneta.IsEnabled = entryCantidadMoto.IsEnabled = false;
        }

        private void ConvertSourceToBytes()
        {

            //  SignaturePadSource = signaturePad.ImageSource;

            if (Imagen.Source != null)
            {
                StreamImageSource streamImageSource = (StreamImageSource)Imagen.Source;


                System.Threading.CancellationToken cancellationToken =
                System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                // store bytes
                // byte[] bytes = new byte[stream.Length];
                //stream.Read(bytes, 0, bytes.Length);

                byte[] imageArray = null;
                byte[] imageArray2 = null;

                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);

                    imageArray = memory.ToArray();
                }

                EstacionamientoImagen = ImageSource.FromStream(() => //
                {                                                    // Convierte de array de bytes a source imagen
                    return new MemoryStream(imageArray);             //
                });                                                  // 

                imageArray2 = imageArray;

                // FotosLista.Add(imageArray2);
            }
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

        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
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


                Imagen.Rotation = 0;


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
                            //_estacionamiento.Jornadas[0].DiaDeLaSemana = dia;
                            lwHorariosLunes.ItemsSource = ListaDeHorariosLunes = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.MARTES:
                            // _estacionamiento.Jornadas[1].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[1].DiaDeLaSemana = dia;
                            lwHorariosMartes.ItemsSource = ListaDeHorariosMartes = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.MIERCOLES:
                            //_estacionamiento.Jornadas[2].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[2].DiaDeLaSemana = dia;
                            lwHorariosMiercoles.ItemsSource = ListaDeHorariosMiercoles = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.JUEVES:
                            //_estacionamiento.Jornadas[3].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[3].DiaDeLaSemana = dia;
                            lwHorariosJueves.ItemsSource = ListaDeHorariosJueves = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.VIERNES:
                            //_estacionamiento.Jornadas[4].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[4].DiaDeLaSemana = dia;
                            lwHorariosViernes.ItemsSource = ListaDeHorariosViernes = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.SABADO:
                            //_estacionamiento.Jornadas[5].Horarios = ListaDeHorarios;
                            //_estacionamiento.Jornadas[5].DiaDeLaSemana = dia;
                            lwHorariosSabado.ItemsSource = ListaDeHorariosSabado = ListaDeHorarios;
                            break;
                        case Model.Enums.Dia.DOMINGO:
                            // _estacionamiento.Jornadas[6].Horarios = ListaDeHorarios;
                            // _estacionamiento.Jornadas[6].DiaDeLaSemana = dia;
                            lwHorariosDomingo.ItemsSource = ListaDeHorariosDomingo = ListaDeHorarios;
                            break;
                    }

                }
            };
        }


        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                activityIndicadorBtnAgregar.IsEnabled = false;
                activityIndicadorBtnAgregar.IsVisible = activityIndicadorBtnAgregar.IsRunning = true;

                if (ListaDeHorariosLunes.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosLunes;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.LUNES;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ListaDeHorariosMartes.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosMartes;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.MARTES;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ListaDeHorariosMiercoles.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosMiercoles;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.MIERCOLES;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ListaDeHorariosJueves.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosJueves;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.JUEVES;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ListaDeHorariosViernes.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosViernes;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.VIERNES;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ListaDeHorariosSabado.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosSabado;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.SABADO;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ListaDeHorariosDomingo.Any())
                {
                    Jornada jornada = new Jornada();
                    jornada.Horarios = ListaDeHorariosDomingo;
                    jornada.DiaDeLaSemana = Model.Enums.Dia.DOMINGO;
                    _estacionamiento.Jornadas.Add(jornada);
                }

                if (ImagenArray != null)
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

                if ((bool)checkBoxAuto.IsChecked && Convert.ToInt32(entryCantidadAuto.Value) != 0)  // AUTO DESDE ACA
                {
                    Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
                    dataVehiculo.TipoDeVehiculo = "Auto";
                    dataVehiculo.CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadAuto.Value);
                    dataVehiculo.Tarifa_Hora = Convert.ToDecimal(entryAuto_TarifaHora.Text);
                    dataVehiculo.Tarifa_Dia = Convert.ToDecimal(entryAuto_TarifaDia.Text);
                    dataVehiculo.Tarifa_Semana = Convert.ToDecimal(entryAuto_TarifaSemana.Text);
                    dataVehiculo.Tarifa_Mes = Convert.ToDecimal(entryAuto_TarifaMes.Text);

                    _estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA
                }
                else if ((bool)checkBoxAuto.IsChecked && Convert.ToInt32(entryCantidadAuto.Value) == 0)
                {
                    await DisplayAlert("Error", "Ha seleccionado Auto como vehiculo a alojar, debe indicar la cantidad de vehiculos de ese tipo que admite en el lugar", "Entendido");
                    return;
                }

                if ((bool)checkBoxCamioneta.IsChecked && Convert.ToInt32(entryCantidadCamioneta.Value) != 0)  // CAMIONETA DESDE ACA
                {
                    Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
                    dataVehiculo.TipoDeVehiculo = "Camioneta";
                    dataVehiculo.CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadCamioneta.Value);
                    dataVehiculo.Tarifa_Hora = Convert.ToDecimal(entryCamioneta_TarifaHora.Text);
                    dataVehiculo.Tarifa_Dia = Convert.ToDecimal(entryCamioneta_TarifaDia.Text);
                    dataVehiculo.Tarifa_Semana = Convert.ToDecimal(entryCamioneta_TarifaSemana.Text);
                    dataVehiculo.Tarifa_Mes = Convert.ToDecimal(entryCamioneta_TarifaMes.Text);

                    _estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA
                }
                else if ((bool)checkBoxCamioneta.IsChecked && Convert.ToInt32(entryCantidadCamioneta.Value) == 0)
                {
                    await DisplayAlert("Error", "Ha seleccionado Camioneta como vehiculo a alojar, debe indicar la cantidad de vehiculos de ese tipo que admite en el lugar", "Entendido");
                    return;
                }

                if ((bool)checkBoxMoto.IsChecked && Convert.ToInt32(entryCantidadMoto.Value) != 0) // MOTO DESDE ACA
                {
                    Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
                    dataVehiculo.TipoDeVehiculo = "Moto";
                    dataVehiculo.CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadMoto.Value);
                    dataVehiculo.Tarifa_Hora = Convert.ToDecimal(entryMoto_TarifaHora.Text);
                    dataVehiculo.Tarifa_Dia = Convert.ToDecimal(entryMoto_TarifaDia.Text);
                    dataVehiculo.Tarifa_Semana = Convert.ToDecimal(entryMoto_TarifaSemana.Text);
                    dataVehiculo.Tarifa_Mes = Convert.ToDecimal(entryMoto_TarifaMes.Text);

                    _estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA
                }
                else if ((bool)checkBoxMoto.IsChecked && Convert.ToInt32(entryCantidadMoto.Value) == 0)
                {
                    await DisplayAlert("Error", "Ha seleccionado Moto como vehiculo a alojar, debe indicar la cantidad de vehiculos de ese tipo que admite en el lugar", "Entendido");
                    return;
                }

                if ((bool)checkBoxMoto.IsChecked == false && (bool)checkBoxAuto.IsChecked == false && (bool)checkBoxCamioneta.IsChecked == false)
                {
                    await DisplayAlert("Error", "Debe seleccionar al menos 1 tipo de vehiculo que admite en el lugar y sus respectivas tarifas y capacidad de dicho vehiculo", "Entendido");
                    return;
                }

                //********************//

                _estacionamiento.MontoReserva = Convert.ToDecimal(entryMontoReserva.Text); // MONTO DE LA RESERVA

                var xxx = _estacionamiento;

                await estacionamientoServiceWebApi.Add(_estacionamiento);
                Tools.Tools.Messages("se agrego el estacionamiento");
                await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
            finally
            {
                activityIndicadorBtnAgregar.IsEnabled = true;
                activityIndicadorBtnAgregar.IsVisible = activityIndicadorBtnAgregar.IsRunning = false;
            }
        }
    }
}