using EasyParking.DTO;
using EasyParking.ViewControllers;
using Model;
using ServiceWebApi;
using ServiceWebApi.DTO;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EasyParking
{
    public partial class App : Application
    {
        public static MensajesViewControllers mensajesView { get; set; } = new MensajesViewControllers();

        public static List<Model.Estacionamiento> Estacionamientos { get; set; } = new List<Model.Estacionamiento>();
        public static CloudData cloudData { get; set; } = new CloudData();
        public static WebApiAccess WebApiAccess { get; set; }

        public static UserInfo UserInfo { get; set; }


        public static NavigationPage _mainPage = null;

        public static string Username { get; set; }
        public static string Password { get; set; }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjcyODEwQDMyMzAyZTMyMmUzMG5CS0JZb1FVSWgxeXR4cml5S1VQb3dJdGJ3YlgvbUl1V3ppNE1VL25ReFk9");

            InitializeComponent();

            cloudData.URLDeAPI = "http://40.118.242.96:12595";

            // MainPage = new NavigationPage(new EasyParking.Views.Menu.MenuContainer(new Inicio()));
            //MainPage = new NavigationPage(new EasyParking.Views.Menu.MenuContainer(new Estacionamiento()));
            //MainPage = new NavigationPage(new Login());
            MainPage = new NavigationPage(new ValidacionLogin());

            // MainPage = new NavigationPage(new Registrarme());
            //MainPage = new NavigationPage(new IniciarSesion());

        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            CheckTokenValidation();
        }

        async void CheckTokenValidation()
        {
            if (Application.Current.Properties.ContainsKey("FechaExpiracionToken") == true)
            {
                var x = Application.Current.Properties["FechaExpiracionToken"] as string;

                DateTime FechaExperitacionToken = Convert.ToDateTime(x);

                DateTime FechaActual = DateTime.Now;

                var horas = Convert.ToInt32((FechaActual - FechaExperitacionToken).TotalHours);

                int result = DateTime.Compare(FechaActual, FechaExperitacionToken);
                string relationship;

                if (result < 0)
                    relationship = "is earlier than"; // 0 > todavia tiene tiempo antes que venza
                else if (result == 0)
                    relationship = "is the same time as"; // = 0 se acaba de vencer
                else
                    relationship = "is later than"; // < 0 lleva rato vencido

                Console.WriteLine("{0} {1} {2}", FechaActual, relationship, FechaExperitacionToken);
                string xx = FechaActual + " " + relationship + " " + FechaExperitacionToken;

                if (result >= 0)
                {
                    // INTENTO LOGEARLO DE NUEVO A LA API PARA QUE SE ACTUALICE EL TOKEN QUE SE VENCIO
                    MsjResultadoDeAccion msjResultado = await Account.Account.LoginAPI(App.cloudData.URLDeAPI, App.cloudData.UsuarioDeAPI, App.cloudData.ContraseñaDeAPI);

                    if (msjResultado.Error) // SI HUBO ERROR LO NOTIFICO CON DISPLAYALERT
                    {
                        await mensajesView.MostrarMensaje(msjResultado.Mensaje);
                    }
                }

                // The example displays the following output for en-us culture:
                //    8/1/2009 12:00:00 AM is earlier than 8/1/2009 12:00:00 PM
            }
        }


    }
}