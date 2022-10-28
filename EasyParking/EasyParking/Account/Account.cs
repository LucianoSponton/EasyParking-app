using Model;
using ServiceWebApi;
using ServiceWebApi.DTO;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EasyParking.Account
{
    public static class Account
    {
        public static async Task<MsjResultadoDeAccion> LoginAPI(string url, string userName, string password)
        {
            MsjResultadoDeAccion msjResultadoDeAccion = new MsjResultadoDeAccion();
            msjResultadoDeAccion.Mensaje = null;

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    App.WebApiAccess = await WebApiAccess.GetAccessAsync(url, userName, password);
                    // var webapiaccess = await WebApiAccess.GetAccessAsync("http://40.118.242.96:12595", "cristiano@hotmail.com", "cristiano123");

                    if (!String.IsNullOrWhiteSpace(App.WebApiAccess.Token)) // ¿HAY TOKEN? SI
                    {
                        Application.Current.Properties.Remove("FechaExpiracionToken"); // Elimino la Fecha de Expiracion del Token vieja
                        await Application.Current.SavePropertiesAsync();               //  

                        var Expiracion = App.WebApiAccess.Expiration;

                        Application.Current.Properties.Add("FechaExpiracionToken", Expiracion.ToString()); // Guardo la Fecha de expiracion del token nueva
                        await Application.Current.SavePropertiesAsync();                                   //

                        if (String.IsNullOrWhiteSpace(App.cloudData.UsuarioDeAPI)) // SI ESTAN VACIAS ES PORQUE PREVIAMENTE NO SE ENCONTRARON CREDENCIALES GUARDADAS LOCALMENTE
                        {
                            App.cloudData.UsuarioDeAPI = userName;
                        }
                        if (String.IsNullOrWhiteSpace(App.cloudData.ContraseñaDeAPI))
                        {
                            App.cloudData.ContraseñaDeAPI = password;
                        }

                        msjResultadoDeAccion.Error = false;

                        return msjResultadoDeAccion;
                    }
                    else
                    {
                        msjResultadoDeAccion.Error = true;
                        msjResultadoDeAccion.Mensaje = App.WebApiAccess.ErrorMessage;
                        return msjResultadoDeAccion;
                    }
                }
                else
                {

                    msjResultadoDeAccion.Error = true;
                    msjResultadoDeAccion.Mensaje = "La conexión a Internet Falló" + "\n" + "Intente de nuevo más tarde";
                    return msjResultadoDeAccion;
                }
            }
            catch (Exception ex)
            {
                //Tools.ExecuteSentry("Tools", Tools.ExtraerNombreMetodo(MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.NombreEXE, App.cloudData.UsuarioDeAPI, App.cloudData.URLDeAPI, App.ModalidadDeLaApp.ToString(), Tools.ExceptionMessage(ex));

                if (ex.Message.Contains("Failed to connect to"))
                {
                    //_mainPage = new NavigationPage(new InternetNotAvailable());
                    msjResultadoDeAccion.Mensaje = "La conexión a Internet Falló" + "\n" + "Intente de nuevo más tarde";
                }
                else if (ex.Message.Contains("(A task was canceled.)"))
                {
                    //_mainPage = new NavigationPage(new ApiNotAvailable());
                    msjResultadoDeAccion.Mensaje = "El Servidor no responde" + "\n" + "Intente de nuevo más tarde";
                }
                else if (ex.Message.Contains("Intento de login NO VALIDO"))
                {

                    //  _mainPage = new NavigationPage(new Login("Email o Contraseña NO VALIDOS", "Intente nuevamente ..."));
                    msjResultadoDeAccion.Mensaje = "Email o Contraseña NO VALIDOS" + "\n" + "Intente nuevamente";
                }
                else if (ex.Message.Contains("HandshakeException"))
                {
                }
                else
                {
                    msjResultadoDeAccion.Mensaje = "Algo salió mal. Inténtalo de nuevo más tarde. \nDetalle del error: \n" + ex.Message;
                }

                msjResultadoDeAccion.Error = true;

                return msjResultadoDeAccion;
            }
        }

        public static async Task<UserInfo> GetUserInfo(string username)
        {
            try
            {
                AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);

                return await accountServiceWebApi.GetUserInfo(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
