using EasyParking.Views.Generales;
using Model;
using Model.Enums;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrarme : ContentPage
    {
        TipoDeDocumento tipoDeDocumento = TipoDeDocumento.NONE;
        TipoDeSexo tipoDeSexo = TipoDeSexo.NONE;

        public Registrarme()
        {
            InitializeComponent();
            comboBoxTipoDeDocumento.Text = "DNI";  // Por defecto se pone a DNI como primera opcion.
            tipoDeDocumento = TipoDeDocumento.DNI; // 
            CargarTipoDeDocumentos();
            comboBoxTipoDeSexo.Text = "HOMBRE";  // Por defecto se pone a HOMBRE como primera opcion.
            tipoDeSexo = TipoDeSexo.HOMBRE; // CargarTipoDeDocumentos();
            CargarTipoDeSexo();
        }

        public void CargarTipoDeDocumentos()
        {
            List<string> ListaTipoDeDocumentos = new List<string>();

            foreach (var item in System.Enum.GetValues(typeof(TipoDeDocumento)))
            {
                if (item.ToString() != TipoDeDocumento.NONE.ToString())
                {
                    ListaTipoDeDocumentos.Add(item.ToString());

                }
            }
            comboBoxTipoDeDocumento.DataSource = ListaTipoDeDocumentos;
        }

        public void CargarTipoDeSexo()
        {
            List<string> ListaTipoDeSexo = new List<string>();

            foreach (var item in System.Enum.GetValues(typeof(TipoDeSexo)))
            {
                if (item.ToString() != TipoDeSexo.NONE.ToString())
                {
                    ListaTipoDeSexo.Add(item.ToString());

                }
            }
            comboBoxTipoDeSexo.DataSource = ListaTipoDeSexo;
        }


        private async void btnRegistrarme_Clicked(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PopCargando());

                if (string.IsNullOrEmpty(entryNombre.Text))
                {
                    await DisplayAlert("Aviso", "Debe ingresar su nombre", "Entendido");
                    return;
                }
                if (string.IsNullOrEmpty(entryApellido.Text))
                {
                    await DisplayAlert("Aviso", "Debe ingresar su apellido", "Entendido");
                    return;
                }
                if (string.IsNullOrEmpty(entryEmail.Text))
                {
                    await DisplayAlert("Aviso", "Debe ingresar su email", "Entendido");
                    return;
                }
                if (string.IsNullOrEmpty(entryContraseña.Text))
                {
                    await DisplayAlert("Aviso", "Debe ingresar una contraseña", "Entendido");
                    return;
                }
                if (string.IsNullOrEmpty(entryConfirmarContraseña.Text))
                {
                    await DisplayAlert("Aviso", "Debe confirmar su contraseña", "Entendido");
                    return;
                }
                if (entryContraseña.Text != entryConfirmarContraseña.Text)
                {
                    await DisplayAlert("Aviso", "Las contraseñas no coinciden", "Entendido");
                    return;
                }
                if (string.IsNullOrEmpty(entryDNI.Text))
                {
                    await DisplayAlert("Aviso", "Debe ingresar su documento", "Entendido");
                    return;
                }



                UserInfo userInfo = new UserInfo();
                userInfo.Nombre = entryNombre.Text.ToLower();
                userInfo.Apellido = entryApellido.Text.ToLower();
                userInfo.Email = entryEmail.Text.ToLower();
                userInfo.NumeroDeDocumento = entryDNI.Text;
                userInfo.TipoDeDocumento = tipoDeDocumento;
                userInfo.Sexo = tipoDeSexo;
                userInfo.Telefono = entryTelefono.Text;
                userInfo.Password = entryContraseña.Text;
                userInfo.FechaDeNacimiento = new DateTime(datePickerFechaDeNacimiento.Date.Year, datePickerFechaDeNacimiento.Date.Month, datePickerFechaDeNacimiento.Date.Day);

                AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);

                await WebApiAccess.CreateUserAsync(App.cloudData.URLDeAPI, userInfo);
                await PopupNavigation.Instance.PopAsync();
                await Navigation.PushAsync(new IniciarSesion());
                DisplayAlert("Registro exitoso", "Ya puede logearse", "Entendido");
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PopAsync();
                await DisplayAlert("Error", Tools.Tools.ExceptionMessage(ex), "Entendido");
            }

        }

        private void comboBoxTipoDeDocumento_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            tipoDeDocumento = (TipoDeDocumento)System.Enum.Parse(typeof(TipoDeDocumento), e.Value.ToString());
        }

        private void comboBoxTipoDeSexo_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            tipoDeSexo = (TipoDeSexo)System.Enum.Parse(typeof(TipoDeSexo), e.Value.ToString());
        }
    }
}