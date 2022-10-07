using EasyParking.DTO;
using Model;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Estacionamientos.MisEstacionamientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupRangoHorario
    {
        List<RangoH> lista = new List<RangoH>();
        List<string> lista2 = new List<string>();

        int id = 0;

        public PopupRangoHorario()
        {
            InitializeComponent();





            //RangoH r1 = new RangoH();
            //RangoH r2 = new RangoH();
            //RangoH r3 = new RangoH();
            //RangoH r4 = new RangoH();


            //r1.Id = id++;
            //r2.Id = id++;
            //r3.Id = id++;
            //r4.Id = id++;

            ////  r2.Id = id++;


            //lista.Add(r1); 
            //lista.Add(r2);
            //lista.Add(r3);
            //lista.Add(r4);

            //   lista.Add(r2);

            //BindingContext = lista;


        }

        public delegate Task MyDelegate(List<string> parameter, List<RangoH> parameter2);
        public event MyDelegate MyEvent;



        private async Task CloseAllPopup(List<string> parametro, List<RangoH> parameter2)
        {

            try
            {
                await MyEvent(parametro, parameter2);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Entendido");
            }
            await PopupNavigation.Instance.PopAsync();


        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            RangoH r = new RangoH();
            var Desde = timePickerDesde.Time;
            var Hasta = timePickerHasta.Time;

            r.DesdeHora = Desde.Hours;
            r.DesdeMinuto = Desde.Minutes;
            r.HastaHora = Hasta.Hours;
            r.HastaMinuto = Hasta.Minutes;
            r.Id = id++;

            string MinDesde = "";
            string MisHasta = "";
            string HorDesde = "";
            string Horasta = "";

            if (Desde.Hours <= 9)
            {
                HorDesde = "0";
            }
            if (Desde.Minutes == 0)
            {
                MinDesde = "0";
            }
            if (Hasta.Hours <= 9)
            {
                Horasta = "0";
            }
            if (Hasta.Minutes == 0)
            {
                MisHasta = "0";
            }

            string result = $"{HorDesde}{r.DesdeHora}:{r.DesdeMinuto}{MinDesde} - {Horasta}{r.HastaHora}:{r.HastaMinuto}{MisHasta}";

            if (lista2.Contains(result))
            {
                await DisplayAlert("Error 🚫", "Este horario ya fue cargado", "Entedio");
            }
            else
            {
                lista.Add(r);
                lista2.Add($"{HorDesde}{r.DesdeHora}:{r.DesdeMinuto}{MinDesde} - {Horasta}{r.HastaHora}:{r.HastaMinuto}{MisHasta}");
                ListaFormatoX.ItemsSource = null;
                ListaFormatoX.ItemsSource = lista2;
            }

        }


        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {

                var btn = sender as ImageButton;
                var detalle = btn?.BindingContext as String;

                int index = 0;
                foreach (var item in lista2)
                {
                    if (item == detalle)
                    {
                        break;
                    }

                    index++;
                }
                lista.Remove(lista.FirstOrDefault(x => x.Id == index));
                lista2.Remove(detalle);

                ListaFormatoX.ItemsSource = null;
                ListaFormatoX.ItemsSource = lista2;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                BindingContext = lista;

            }
        }



        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            await CloseAllPopup(lista2, lista);
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}