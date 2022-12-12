using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Generales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Busquedas : ContentPage
    {

        ServiceWebApi.EstacionamientoServiceWebApi estacionamientoServiceWebApi = new ServiceWebApi.EstacionamientoServiceWebApi(App.WebApiAccess);
        public Busquedas(string busquedaAnterios = "")
        {
            InitializeComponent();
            searchBar.Text = busquedaAnterios;
            searchBar.Focus();
        }

        public delegate Task MyDelegate(int parameter);
        public event MyDelegate MyEvent;

        private async Task CloseAllPopup(int parametro)
        {

            try
            {
                await MyEvent(parametro);
            }
            catch (Exception ex)
            {
                // Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("ERROR ...", Tools.Tools.TraduceError(ex), "Entendido");
            }
            await Navigation.PopAsync();


        }





        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();


                await Buscar(searchBar.Text);

                searchBar.Focus();

            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }

        async Task Buscar(string texto)
        {
            try
            {
                if (!string.IsNullOrEmpty(texto))
                {
                    lwlistado.ItemsSource = null;
                    lwlistado.ItemsSource = await estacionamientoServiceWebApi.GetTags(texto);
                }
            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                await DisplayAlert("ERROR ...", Tools.Tools.TraduceError(ex), "Entendido");
            }
        }



        private async void lwlistado_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try
            {
                activityIndicator.IsVisible = true;
                var detalle = e.ItemData as ServiceWebApi.DTO.Tag;

                await CloseAllPopup(detalle.EstacionamientoId);

            }
            catch (Exception ex)
            {
                //Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

                throw ex;
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();

        }

        private async void searchBar_PropertyChanged_1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            await Buscar(searchBar.Text);
        }




        private async void btnAddTextoToSearchBar_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    var btn = sender as ImageButton;
            //    var detalle = btn?.BindingContext as LS.eCommerce.Domain.Model.SearchTag;
            //    searchBar.Text = detalle.Tag;
            //}
            //catch (Exception ex)
            //{
            //    Tools.Tools.ExecuteSentry(App.NombreEmpresa, Tools.Tools.GetPackageInfo(), Tools.Tools.GetVersionCode(), this.GetType().Name, Tools.Tools.ExtraerNombreMetodo(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name), App.cloudData.UsuarioDeAPI, App.webApiUri, await Tools.Tools.ExceptionMessage(ex), DateTime.Now);

            //    await DisplayAlert("Error", Tools.Tools.TraduceError(ex), "Entendido");
            //}
        }

        private async void searchBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await Buscar(searchBar.Text);
        }
    }
}