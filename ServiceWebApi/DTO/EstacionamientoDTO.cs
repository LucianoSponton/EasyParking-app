using Model;
using System;
using System.Collections.Generic;
using System.Text;
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

        public bool _publicacionPausada { get; set; }

        public bool PublicacionPausada
        {
            get { return _publicacionPausada; }
            set
            {
                _publicacionPausada = value;
                OnPropertyChanged();

            }
        }
    }
}
