using EasyParking.Interfaces;
using EasyParking.Views.Generales;
using EasyParking.Views.Menu;
using Rg.Plugins.Popup.Services;
using ServiceWebApi.DTO;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IniciarSesion : ContentPage
    {
        string username;
        string password;

        public IniciarSesion()
        {
            InitializeComponent();
        }

        private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var p = new PopCargando();
                await PopupNavigation.Instance.PushAsync(p);

                if (!string.IsNullOrEmpty(entryEmail.Text) & !string.IsNullOrEmpty(entryContraseña.Text))
                {


                    username = entryEmail.Text.Trim();
                    password = entryContraseña.Text.Trim();

                    MsjResultadoDeAccion msjResultadoDeAccion = await Account.Account.LoginAPI(App.cloudData.URLDeAPI, username, password);

                    if (!msjResultadoDeAccion.Error) // LOGIN EXITOSO
                    {
                        App.cloudData.UsuarioDeAPI = entryEmail.Text;
                        App.cloudData.ContraseñaDeAPI = entryContraseña.Text;


                        if (Application.Current.Properties.ContainsKey("UsuarioDeAPI") == true)
                        {
                            Application.Current.Properties.Remove("UsuarioDeAPI");
                        }
                        if (Application.Current.Properties.ContainsKey("ContraseñaDeAPI") == true)
                        {
                            Application.Current.Properties.Remove("ContraseñaDeAPI");
                        }

                        Application.Current.Properties.Add("UsuarioDeAPI", username);
                        Application.Current.Properties.Add("ContraseñaDeAPI", password);
                        await Application.Current.SavePropertiesAsync();

                        App.UserInfo = await Account.Account.GetUserInfo(App.cloudData.UsuarioDeAPI);  // Obtengo los datos del usuario para trabajar en la app

                        Application.Current.MainPage = new NavigationPage(new MenuContainer(new Inicio.Inicio()));

                        //await PopupNavigation.Instance.PopAsync();

                    } // FALLO EL LOGIN, NOTIFICO UN MSJ
                    else
                    {
                        DisplayAlert("Error", msjResultadoDeAccion.Mensaje, "Entendido");
                    }

                }
                else // TIENE QUE COMPLETAR ALGUN CAMPO QUE FALTA
                {
                    DependencyService.Get<IMessage>().Longtime("Todos los campos deben ser completados");
                }
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);
                //await PopupNavigation.Instance.PopAsync();

                DisplayAlert("Error", ex.Message, "Entendido");
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }

        }
    }
}