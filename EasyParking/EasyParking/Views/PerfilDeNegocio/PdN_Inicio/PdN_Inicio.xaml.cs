using EasyParking.DTO;
using EasyParking.Views.Estacionamientos.MisEstacionamientos;
using EasyParking.Views.Generales;
using EasyParking.Views.PerfilDeNegocio.Tarifas.Tarifa;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.PerfilDeNegocio.PdN_Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdN_Inicio : ContentPage
    {
        public PdN_Inicio()
        {
            InitializeComponent();

            List<ItemDelMenu> ListaItemsDelMenu = new List<ItemDelMenu>();

            int id = 1;
            ItemDelMenu itemMisEstacionamientos = new ItemDelMenu
            {
                Descripcion = "Mis estacionamientos",
                Icono = "SignalParking.png",
                Id = id++
            };

            ListaItemsDelMenu.Add(itemMisEstacionamientos);

            ItemDelMenu itemTarifas = new ItemDelMenu
            {
                Descripcion = "Tarifas",
                Icono = "tarifas.png",
                Id = id++
            };

            //  ListaItemsDelMenu.Add(itemTarifas);

            ItemDelMenu itemReservasActuales = new ItemDelMenu
            {
                Descripcion = "Reservas actuales de clientes",
                Icono = "Reserva.png",
                Id = id++
            };

            ListaItemsDelMenu.Add(itemReservasActuales);

            ItemDelMenu itemMarcarSalida = new ItemDelMenu
            {
                Descripcion = "Marcar Salida",
                Icono = "exit.png",
                Id = id++
            };

            ListaItemsDelMenu.Add(itemMarcarSalida);

            ItemDelMenu itemHistorialDeReservas = new ItemDelMenu
            {
                Descripcion = "Historial de reservas",
                Icono = "expediente.png",
                Id = id++
            };

            ListaItemsDelMenu.Add(itemHistorialDeReservas);



            lwlistado.ItemsSource = ListaItemsDelMenu;


        }

        private async void lwlistado_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

            var myselecteditem = e.ItemData as ItemDelMenu;

            switch (myselecteditem.Id)
            {
                case 1:
                    await Navigation.PushAsync(new MisEstacionamientos());
                    break;
                case 2:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new Tarifa(true, true, true));
                    await PopupNavigation.Instance.PopAsync();
                    break;
                case 3:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new PerfilDeNegocio.PdN_Reservas.PdN_Reservas());
                    await PopupNavigation.Instance.PopAsync();
                    break;
                case 4:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new PerfilDeNegocio.MarcarSalida.MarcarSalida());
                    await PopupNavigation.Instance.PopAllAsync();
                    break;
                case 5:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new PerfilDeNegocio.HistorialDeReservas.HistorialDeReservas());
                    await PopupNavigation.Instance.PopAllAsync();
                    break;

            }
        }
    }
}