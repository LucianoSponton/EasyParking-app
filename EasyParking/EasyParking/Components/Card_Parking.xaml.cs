using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Card_Parking : ContentView
    {
        public Card_Parking()
        {
            InitializeComponent();
        }

        public delegate void OnClicked_ReservaDelegate();
        public event EventHandler OnClicked_Reserva;

        public delegate void OnClicked_VerMasDelegate();
        public event EventHandler OnClicked_VerMas;

        public delegate void OnClicked_VerMapaDelegate();
        public event EventHandler OnClicked_VerMapa;

        public delegate void OnClicked_VeImagenDelegate();
        public event EventHandler OnClicked_VeImagen;

        public delegate void OnClicked_FavoritoDelegate();
        public event EventHandler OnClicked_Favorito;

        private bool _isVisibleFooter { get; set; }

        public bool IsVisibleFooter
        {
            get { return _isVisibleFooter; }
            set
            {
                stackFooter.IsVisible = value;
            }
        }


        private void btnReserva_Clicked(object sender, EventArgs e)
        {
            OnClicked_Reserva?.Invoke(this, EventArgs.Empty);
        }

        private void btnVerMas_Clicked(object sender, EventArgs e)
        {
            OnClicked_VerMas?.Invoke(this, EventArgs.Empty);
        }

        private void btnVerMapa_Clicked(object sender, EventArgs e)
        {
            OnClicked_VerMapa?.Invoke(this, EventArgs.Empty);
        }


        private void bntVerImagen_Clicked(object sender, EventArgs e)
        {
            OnClicked_VeImagen?.Invoke(this, EventArgs.Empty);
        }

        private void btnFavorito_Clicked(object sender, EventArgs e)
        {
            OnClicked_Favorito?.Invoke(this, EventArgs.Empty);
        }
    }
}