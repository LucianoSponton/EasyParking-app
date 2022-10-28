
using EasyParking.Views.Generales;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.MiCuenta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiCuenta : ContentPage
    {
        Stream STREAM;
        byte[] ImagenArray = null;

        public ImageSource EstacionamientoImagen { get; set; }

        public MiCuenta()
        {
            InitializeComponent();

            if (App.UserInfo.FotoDePerfil != null)
            {
                imagenDePerfil.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(App.UserInfo.FotoDePerfil);
                });
            }



        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            try
            {
                //App.UserInfo = await Account.Account.GetUserInfo(App.cloudData.UsuarioDeAPI);

                if (App.UserInfo != null)
                {
                    if (!string.IsNullOrEmpty(App.UserInfo.Apodo))
                    {
                        labelNombreyApellido.Text = App.UserInfo.Apodo;
                    }
                    else
                    {
                        labelNombreyApellido.Text = App.UserInfo.Nombre + " " + App.UserInfo.Apellido;
                    }
                    labelEmail.Text = App.UserInfo.Email;
                }

            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.ExceptionMessage(ex), "Entendido");
            }
        }

        private async void btnEditarNombre_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopCargando());
            await Navigation.PushAsync(new EditarNombre(App.UserInfo));
            await PopupNavigation.Instance.PopAsync();
        }

        private async void btnEliminarCuenta_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Advertencia", "¿Seguro desea eliminar su cuenta?", "Eliminar", "Cancelar");
            if (result)
            {
                try
                {
                    await PopupNavigation.Instance.PushAsync(new PopCargando());

                    AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);

                    await accountServiceWebApi.UserLock(App.cloudData.UsuarioDeAPI);
                    BorrarCredenciales();
                    Application.Current.MainPage = new NavigationPage(new EasyParking.Views.Login.Login());
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", Tools.Tools.ExceptionMessage(ex), "Entendido");
                }
                finally
                {
                    await PopupNavigation.Instance.PopAsync();
                }
            }
        }

        private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Aviso", "¿Desea cerrar sesión?", "Si, cerrar", "Cancelar"))
            {
                BorrarCredenciales();

                this.Navigation.InsertPageBefore(new Login.Login(), this.Navigation.NavigationStack[0]);

                var existingPages = this.Navigation.NavigationStack.ToList();
                this.Navigation.RemovePage(this.Navigation.NavigationStack[1]);
                await this.Navigation.PopAsync();
            }
        }

        async void BorrarCredenciales()
        {

            Application.Current.Properties.Remove("UsuarioDeAPI");
            Application.Current.Properties.Remove("ContraseñaDeAPI");
            await Application.Current.SavePropertiesAsync();

            if (Application.Current.Properties.ContainsKey("UsuarioDeAPI") == false)
            {
                Application.Current.Properties.Add("UsuarioDeAPI", "");
                await Application.Current.SavePropertiesAsync();
            }
            if (Application.Current.Properties.ContainsKey("ContraseñaDeAPI") == false)
            {
                Application.Current.Properties.Add("ContraseñaDeAPI", "");
                await Application.Current.SavePropertiesAsync();
            }
        }

        private async void btnInformacionPersonal_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopCargando());
            await Navigation.PushAsync(new InformacionPersonal(App.UserInfo));
            await PopupNavigation.Instance.PopAsync();
        }

        private async void btnImagenDePerfil_Clicked(object sender, EventArgs e)
        {
            await TomarFoto();
            //SeleccionarFoto();
        }

        private async Task<byte[]> ConvertSourceToBytes(FileResult result, ImageSource IM)
        {


            if (imagenDePerfil.Source != null)
            {

                StreamImageSource streamImageSource = (StreamImageSource)ImageSource.FromStream(() => STREAM);
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                STREAM = await result.OpenReadAsync();
                imagenDePerfil.Source = ImageSource.FromStream(() => STREAM);

                byte[] imageArray = null;

                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    imageArray = memory.ToArray();
                }

                return imageArray;
            }
            else
            {
                return null;
            }
        }

        private async Task<byte[]> ConvertSourceToBytes(MediaFile result, ImageSource IM)
        {


            if (imagenDePerfil.Source != null)
            {

                StreamImageSource streamImageSource = (StreamImageSource)IM;
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                var stream2 = result.GetStream();
                imagenDePerfil.Source = ImageSource.FromStream(() => stream2);

                byte[] imageArray = null;

                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    imageArray = memory.ToArray();
                }

                return imageArray;
            }
            else
            {
                return null;
            }
        }

        async Task TomarFoto()
        {

            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                STREAM = await result.OpenReadAsync();

                imagenDePerfil.Source = ImageSource.FromStream(() => STREAM);
                imagenDePerfil.Rotation = 90;
                ImageSource IM = imagenDePerfil.Source;
                App.UserInfo.FotoDePerfil = await ConvertSourceToBytes(result, IM);
                if (App.UserInfo.FotoDePerfil != null)
                {
                    AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);
                    await accountServiceWebApi.Update(App.UserInfo);
                    Tools.Tools.Messages("Se guardaron los cambios");
                }


            }
        }


        async void SeleccionarFoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Seleccion de fotos NO permitida", "Se require permisos para seleccionar fotos.", "Entendido");
                return;
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,

            });


            if (file == null)
            {
                return;

            }


            imagenDePerfil.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            App.UserInfo.FotoDePerfil = await ConvertSourceToBytes(file, imagenDePerfil.Source);
            if (App.UserInfo.FotoDePerfil != null)
            {
                AccountServiceWebApi accountServiceWebApi = new AccountServiceWebApi(App.WebApiAccess);
                await accountServiceWebApi.Update(App.UserInfo);
                Tools.Tools.Messages("Se guardaron los cambios");
            }
        }

    }
}