using Model;
using ServiceWebApi;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.MiCuenta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarNombre : ContentPage
    {
        private UserInfo _userInfo;
        AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);

        public EditarNombre(UserInfo userInfo)
        {
            InitializeComponent();
            _userInfo = userInfo;
            entryApellido.Text = userInfo.Apellido;
            entryNombre.Text = userInfo.Nombre;
            if (!string.IsNullOrEmpty(userInfo.Apodo))
            {
                entryApodo.Text = userInfo.Apodo;
            }
        }

        private async void btnGuardarCambios_Clicked(object sender, EventArgs e)
        {
            try
            {

                _userInfo.Apodo = entryApodo.Text.Trim();

                await accountServiceWebApi.Update(_userInfo);
                Tools.Tools.Messages("Se guardaron los cambios");
                App.UserInfo = await accountServiceWebApi.GetUserInfo(_userInfo.UserName);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.ExceptionMessage(ex), "Entendido");
            }
        }
    }
}