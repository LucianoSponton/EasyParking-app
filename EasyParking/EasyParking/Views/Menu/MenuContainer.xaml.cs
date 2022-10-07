using EasyParking.MenuItems;
using EasyParking.Views.Generales;
using EasyParking.Views.PerfilDeNegocio.PdN_Inicio;
using EasyParking.Views.Reservas.MisReservas;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyParking.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuContainer : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        MasterPageItem page1;
        MasterPageItem page2;
        MasterPageItem page3;
        MasterPageItem page4;
        MasterPageItem page5;
        MasterPageItem page6;



        public MenuContainer(Page pagina)
        {
            InitializeComponent();



            if (App.UserInfo != null)
            {
                if (!string.IsNullOrEmpty(App.UserInfo.Apodo))
                {
                    labelNombreDeUsuario.Text = App.UserInfo.Apodo;
                }
                else
                {
                    labelNombreDeUsuario.Text = App.UserInfo.Nombre + " " + App.UserInfo.Apellido;
                }
            }

            menuList = new List<MasterPageItem>();


            MessagingCenter.Subscribe<string>(this, "DisableGesture", (sender) =>
            {
                if (sender == "0")
                {
                    IsGestureEnabled = false;
                }
                else
                {
                    IsGestureEnabled = true;
                }
            });

            page1 = new MasterPageItem() { id = 1, Title = "Mis reservas", Icon = "Reserva.png" };
            page2 = new MasterPageItem() { id = 2, Title = "Favoritos", Icon = "heart.png" };
            page3 = new MasterPageItem() { id = 3, Title = "Mis vehículos", Icon = "key.png" };
            page4 = new MasterPageItem() { id = 4, Title = "Perfil de negocio", Icon = "perfil.png" };
            page5 = new MasterPageItem() { id = 5, Title = "Mi cuenta", Icon = "usuario.png" };
            menuList.Add(page1);
            menuList.Add(page2);
            menuList.Add(page3);
            menuList.Add(page4);
            menuList.Add(page5);

            navigationDrawerList.ItemsSource = menuList;
            Detail = new NavigationPage(pagina);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.UserInfo.FotoDePerfil != null)
            {
                imagenDePerfil.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(App.UserInfo.FotoDePerfil);
                });

                imagenDePerfil.Rotation = 90;
            }
        }

        private async void navigationDrawerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myselecteditem = e.Item as MasterPageItem;

            switch (myselecteditem.id)
            {
                case 1:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new MisReservas());
                    await PopupNavigation.Instance.PopAsync();
                    break;
                case 2:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new EasyParking.Views.Favoritos.Favoritos());
                    await PopupNavigation.Instance.PopAsync();
                    break;
                case 3:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new MisVehiculos.MisVehiculos());
                    await PopupNavigation.Instance.PopAllAsync();
                    break;
                case 4:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new PdN_Inicio());
                    await PopupNavigation.Instance.PopAllAsync();
                    break;
                case 5:
                    await PopupNavigation.Instance.PushAsync(new PopCargando());
                    await Navigation.PushAsync(new MiCuenta.MiCuenta());
                    await PopupNavigation.Instance.PopAllAsync();
                    break;
            }
            ((ListView)sender).SelectedItem = null;
            IsPresented = false;
        }
    }
}