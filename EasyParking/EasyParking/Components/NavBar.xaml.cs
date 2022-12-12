using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavBar : ContentView
    {
        public NavBar()
        {
            InitializeComponent();
        }


        public delegate void OnClicked_FavoritoDelegate();
        public event EventHandler OnClicked_Favorito;

        public delegate void OnClicked_CarritoDelegate();
        public event EventHandler OnClicked_Carrito;

        public delegate void SearchBar_FocusedDelegate();
        public event EventHandler SearchBar_Focused;

        public delegate void SearchBar_ButtonPressedDelegate();
        public event EventHandler SearchBar_ButtonPressed;

        public string Title
        {
            get { return TituloDePagina.Text; }
            set
            {
                TituloDePagina.Text = value;
            }
        }
        public string SearchBar_Text
        {
            get { return searchBar.Text; }
            set
            {
                searchBar.Text = value;
            }
        }

        private bool _isVisibleFavoritos { get; set; }

        public bool IsVisibleFavoritos
        {
            get { return _isVisibleFavoritos; }
            set
            {
                BtnFavoritos.IsVisible = value;
            }
        }

        private bool _isVisibleBoxSearch { get; set; }

        public bool IsVisibleBoxSearch
        {
            get { return _isVisibleBoxSearch; }
            set
            {
                BoxSearchBar.IsVisible = value;
            }
        }

        private bool _isVisibleTituloDePagina { get; set; }

        public bool IsVisibleTituloDePagina
        {
            get { return _isVisibleTituloDePagina; }
            set
            {
                TituloDePagina.IsVisible = value;
            }
        }

        private void searchBar_Focused(object sender, FocusEventArgs e)
        {
            SearchBar_Focused?.Invoke(this, EventArgs.Empty);
        }

        private void BtnFavoritos_Clicked(object sender, EventArgs e)
        {
            OnClicked_Favorito?.Invoke(this, EventArgs.Empty);
        }

        private void btnCarrito_Clicked(object sender, EventArgs e)
        {
            OnClicked_Carrito?.Invoke(this, EventArgs.Empty);
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            SearchBar_ButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}