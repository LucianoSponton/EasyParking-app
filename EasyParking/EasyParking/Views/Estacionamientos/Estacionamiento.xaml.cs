using EasyParking.DTO;
using EasyParking.Views.Estacionamientos.MisEstacionamientos;
using EasyParking.Views.PerfilDeNegocio.Tarifas.Tarifa;
using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
        Model.Estacionamiento estacionamiento = new Model.Estacionamiento();

        Stream STREAM;
        List<ImageSource> ImageSourceLista = new List<ImageSource>();

        public ImageSource EstacionamientoImagen { get; set; }
        public Estacionamiento()
        {
            InitializeComponent();
            frameTarifaAuto.IsVisible = frameTarifaCamioneta.IsVisible = frameTarifaMoto.IsVisible = false;
            entryCantidadAuto.IsEnabled = entryCantidadCamioneta.IsEnabled = entryCantidadMoto.IsEnabled = false;


            List<Horario> PeriodosLunes = new List<Horario>();

            Horario h = new Horario
            {
                Periodo = "8:00 - 12:30"
            };
            Horario h2 = new Horario
            {
                Periodo = "14:00 - 12:30"
            };
            Horario h3 = new Horario
            {
                Periodo = "9:00 - 12:30"
            };
            PeriodosLunes.Add(h);
            PeriodosLunes.Add(h2);
            PeriodosLunes.Add(h3);

            //lwHorariosLunes.ItemsSource = new string[]
            //                                {
            //                                  h.Periodo,
            //                                  h2.Periodo,
            //                                    h2.Periodo,
            //                                  h3.Periodo
            //                                };

            //lwHorariosMartes.ItemsSource = new string[]
            //                               {
            //                                  h3.Periodo
            //                               };

            //lwHorariosMiercoles.ItemsSource = new string[]
            //                               {
            //                                  h.Periodo,
            //                                  h3.Periodo
            //                               };

            //lwHorariosSabado.ItemsSource = new string[]
            //                               {
            //                                  h.Periodo,
            //                                  h3.Periodo
            //                               };


        }

        public class Horario
        {
            public string Periodo { get; set; }
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




        private void comboBoxTipoDeEstado_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        private void comboBoxTipoDeLugar_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        /// /////// No se usan /// ///////
        private async void btnAutoTarifa_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupTarifa("Auto"));
        }

        private async void btnCamionetaTarifa_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupTarifa("Camioneta"));
        }

        private async void btnMotoTarifa_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupTarifa("Moto"));
        }
        /// /////// //////// /// ///////

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
                            estacionamiento.DiasConHorarios[0].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[0].DiaDeLaSemana = dia;
                            lwHorariosLunes.ItemsSource = respuesta;
                            break;
                        case Model.Enums.Dia.MARTES:
                            estacionamiento.DiasConHorarios[1].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[1].DiaDeLaSemana = dia;
                            lwHorariosMartes.ItemsSource = respuesta;
                            break;
                        case Model.Enums.Dia.MIERCOLES:
                            estacionamiento.DiasConHorarios[2].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[2].DiaDeLaSemana = dia;
                            lwHorariosMiercoles.ItemsSource = respuesta;
                            break;
                        case Model.Enums.Dia.JUEVES:
                            estacionamiento.DiasConHorarios[3].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[3].DiaDeLaSemana = dia;
                            lwHorariosJueves.ItemsSource = respuesta;
                            break;
                        case Model.Enums.Dia.VIERNES:
                            estacionamiento.DiasConHorarios[4].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[4].DiaDeLaSemana = dia;
                            lwHorariosViernes.ItemsSource = respuesta;
                            break;
                        case Model.Enums.Dia.SABADO:
                            estacionamiento.DiasConHorarios[5].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[5].DiaDeLaSemana = dia;
                            lwHorariosSabado.ItemsSource = respuesta;
                            break;
                        case Model.Enums.Dia.DOMINGO:
                            estacionamiento.DiasConHorarios[6].Horarios = ListaDeHorarios;
                            estacionamiento.DiasConHorarios[6].DiaDeLaSemana = dia;
                            lwHorariosDomingo.ItemsSource = respuesta;
                            break;
                    }

                }
            };
        }



        private void btnAgregar_Clicked(object sender, EventArgs e)
        {

            estacionamiento.Imagen = ImagenArray; // IMAGEN DEL LUGAR

            estacionamiento.Ciudad = comboboxCiudad.Text; // CIUDAD DEL LUGAR

            estacionamiento.Nombre = entryNombre.Text; // NOMBRE DEL LUGAR

            estacionamiento.Direccion = entryDireccion.Text; // DIRECCION DEL LUGAR

            estacionamiento.TipoDeLugar = comboBoxTipoDeLugar.Text; // TIPO DEL LUGAR

            // LOS RANGO HORARIOS YA SE CARGARON ANTES EN EL EVENTO --> btnEditarHorario_Clicked

            //********** TEMA VEHICULOS ACEPTADOS Y SUS TARIFAS **********//

            if ((bool)checkBoxAuto.IsChecked && Convert.ToInt32(entryCantidadAuto.Value) != 0)  // AUTO DESDE ACA
            {
                Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
                dataVehiculo.TipoDeVehiculo = "Auto";
                dataVehiculo.CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadAuto.Value);
                dataVehiculo.Tarifa_Hora = Convert.ToInt32(entryAuto_TarifaHora.Text);
                dataVehiculo.Tarifa_Dia = Convert.ToInt32(entryAuto_TarifaDia.Text);
                dataVehiculo.Tarifa_Semana = Convert.ToInt32(entryAuto_TarifaSemana.Text);
                dataVehiculo.Tarifa_Mes = Convert.ToInt32(entryAuto_TarifaMes.Text);

                estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA
            }


            if ((bool)checkBoxCamioneta.IsChecked && Convert.ToInt32(entryCantidadCamioneta.Value) != 0)  // CAMIONETA DESDE ACA
            {
                Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
                dataVehiculo.TipoDeVehiculo = "Camioneta";
                dataVehiculo.CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadCamioneta.Value);
                dataVehiculo.Tarifa_Hora = Convert.ToInt32(entryCamioneta_TarifaHora.Text);
                dataVehiculo.Tarifa_Dia = Convert.ToInt32(entryCamioneta_TarifaDia.Text);
                dataVehiculo.Tarifa_Semana = Convert.ToInt32(entryCamioneta_TarifaSemana.Text);
                dataVehiculo.Tarifa_Mes = Convert.ToInt32(entryCamioneta_TarifaMes.Text);

                estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA
            }


            if ((bool)checkBoxMoto.IsChecked && Convert.ToInt32(entryCantidadMoto.Value) != 0) // MOTO DESDE ACA
            {
                Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
                dataVehiculo.TipoDeVehiculo = "Moto";
                dataVehiculo.CapacidadDeAlojamiento = Convert.ToInt32(entryCantidadMoto.Value);
                dataVehiculo.Tarifa_Hora = Convert.ToInt32(entryMoto_TarifaHora.Text);
                dataVehiculo.Tarifa_Dia = Convert.ToInt32(entryMoto_TarifaDia.Text);
                dataVehiculo.Tarifa_Semana = Convert.ToInt32(entryMoto_TarifaSemana.Text);
                dataVehiculo.Tarifa_Mes = Convert.ToInt32(entryMoto_TarifaMes.Text);

                estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA
            }

            //********************//


            estacionamiento.MontoReserva = Convert.ToDecimal(entryMontoReserva.Text); // MONTO DE LA RESERVA

            var xxx = estacionamiento;

            App.Estacionamientos.Add(estacionamiento);

        }

        private void comboboxCiudad_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }
    }
}