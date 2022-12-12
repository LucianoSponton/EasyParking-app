using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ServiceWebApi.DTO
{
    public class EstacionamientoDTO : Estacionamiento, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Distancia { get; set; }

        private bool _favorito { get; set; }
        public bool Favorito
        {
            get { return _favorito; }
            set
            {
                if (_favorito != value)
                {
                    _favorito = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imagenFavorito { get; set; }
        public string ImagenFavorito
        {
            get { return _imagenFavorito; }
            set
            {
                if (_imagenFavorito != value)
                {
                    _imagenFavorito = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
