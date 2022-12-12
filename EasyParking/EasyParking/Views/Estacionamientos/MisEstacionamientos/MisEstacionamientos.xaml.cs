using EasyParking.Views.Generales;
using Rg.Plugins.Popup.Services;
using ServiceWebApi;
using Syncfusion.XForms.Buttons;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Estacionamientos.MisEstacionamientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisEstacionamientos : ContentPage
    {
        EstacionamientoServiceWebApi estacionamientoServiceWebApi;

        public MisEstacionamientos()
        {
            InitializeComponent();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                estacionamientoServiceWebApi = new EstacionamientoServiceWebApi(App.WebApiAccess);

                activityIndicator.IsVisible = true;
                lwlisado.ItemsSource = null;
                var Lista = await estacionamientoServiceWebApi.GetMisEstacionamientos();
                lwlisado.ItemsSource = Lista;
                activityIndicator.IsVisible = false;

                if (Lista.Count == 0)
                {
                    BoxNotSearchResult.IsVisible = true;
                }
                else
                {
                    BoxNotSearchResult.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }

        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopCargando());
            await Navigation.PushAsync(new Estacionamiento());
            await PopupNavigation.Instance.PopAsync();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Eliminar Estacionamiento", "¿seguro desea eliminarlo?", "Si, eliminar", "No");

            if (result)
            {
                try
                {
                    var detalle = Tools.Tools.DarFormaAObjeto(sender);
                    await estacionamientoServiceWebApi.SetInactivo(detalle.Id);
                    Tools.Tools.Messages("Estacionamiento eliminado");
                    activityIndicator.IsVisible = true;
                    lwlisado.ItemsSource = null;
                    lwlisado.ItemsSource = await estacionamientoServiceWebApi.GetMisEstacionamientos();
                    activityIndicator.IsVisible = false;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
                }
            }
        }

        private async void imagenCard_Clicked(object sender, EventArgs e)
        {

            try
            {
                var btn = sender as ImageButton;
                var detalle = btn?.BindingContext as Model.Estacionamiento;
                var p = new PopupItemImage(detalle.Imagen);
                await PopupNavigation.Instance.PushAsync(p);
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        private async void btnPausarReanudarPublicacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as SfButton;
                var detalle = btn?.BindingContext as Model.Estacionamiento;
                bool result;



                if (detalle.PublicacionPausada == false)
                {
                    result = await DisplayAlert("Pausar publicación", "¿seguro desea pausarla?", "Si, pausar", "No");
                }
                else
                {
                    result = await DisplayAlert("Reanudar publicación", "¿seguro desea reanudarla?", "Si, reanudar", "No");
                }


                if (result)
                {
                    if (detalle.PublicacionPausada) // Si entra aca es porque la publicacion estaba pausada, entonces ahora la activo
                    {
                        await estacionamientoServiceWebApi.SetReanudarPublicacion(detalle.Id);
                        detalle.PublicacionPausada = true;

                    }
                    else  // Si entra aca es porque la publicacion estaba activa, entonces ahora la pauso
                    {
                        await estacionamientoServiceWebApi.SetPublicacionPausada(detalle.Id);
                        detalle.PublicacionPausada = false;
                    }

                    activityIndicator.IsVisible = true;
                    lwlisado.ItemsSource = null;
                    lwlisado.ItemsSource = await estacionamientoServiceWebApi.GetMisEstacionamientos();
                    activityIndicator.IsVisible = false;

                }


            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ImageButton;
                var detalle = btn?.BindingContext as Model.Estacionamiento;

                await PopupNavigation.Instance.PushAsync(new PopCargando());
                //await Navigation.PushAsync(new EditarEstacionamiento(detalle.Id));
                await Navigation.PushAsync(new EditarEstacionamiento(detalle));
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}