using EasyParking.Views.Generales;
using Model;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.MiCuenta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacionPersonal : ContentPage
    {
        private UserInfo _userInfo;

        public InformacionPersonal(UserInfo userInfo)
        {
            InitializeComponent();
            _userInfo = userInfo;
            CargarUserInfo(userInfo);
        }

        void CargarUserInfo(UserInfo userInfo)
        {
            entryApellido.Text = userInfo.Apellido;
            entryNombre.Text = userInfo.Nombre;
            entryEmail.Text = userInfo.Email;
            entryTelefono.Text = userInfo.Telefono;
            entryDNI.Text = userInfo.NumeroDeDocumento;
            entryTipoDocumento.Text = userInfo.TipoDeDocumento.ToString();
            entryFechaDeNacimiento.Text = $"{userInfo.FechaDeNacimiento.Day}/{userInfo.FechaDeNacimiento.Month}/{userInfo.FechaDeNacimiento.Year}";
        }


        private async void btnEditartelefono_Clicked(object sender, EventArgs e)
        {
            var p = new PopupEditSimpleData();
            await PopupNavigation.Instance.PushAsync(p);
            p.MyEvent += async (args) =>
            {
                var respuesta = args;
                if (!string.IsNullOrEmpty(respuesta))
                {
                    try
                    {
                        AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);


                        App.UserInfo.Telefono = respuesta; // asigno el nuevo telefono

                        await accountServiceWebApi.Update(App.UserInfo); // envio la modificacion
                        Tools.Tools.Messages("Se guardaron los cambios");
                        App.UserInfo = await accountServiceWebApi.GetUserInfo(App.cloudData.UsuarioDeAPI);
                        CargarUserInfo(App.UserInfo);
                    }
                    catch (Exception ex)
                    {
                        //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                        await DisplayAlert("Error", Tools.Tools.ExceptionMessage(ex), "Entendido");
                    }
                }
            };
        }
    }
}