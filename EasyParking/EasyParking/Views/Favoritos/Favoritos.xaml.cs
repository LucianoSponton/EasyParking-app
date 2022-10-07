
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Favoritos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favoritos : ContentPage
    {
        public Favoritos()
        {
            InitializeComponent();

            lwlisado.ItemsSource = null;
            List<Model.Estacionamiento> lista;

            lista = Tools.Tools.GetEstacionamientosMock();
            lista[0].Favorito = true;
            lista[1].Favorito = true;
            lista.RemoveAt(2);
            lwlisado.ItemsSource = lista;

        }
    }
}