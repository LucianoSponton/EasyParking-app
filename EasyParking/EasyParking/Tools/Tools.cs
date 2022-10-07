using EasyParking.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace EasyParking.Tools
{
    public static class Tools
    {

        public static byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }



        public static List<Model.Estacionamiento> GetEstacionamientosMock()
        {
            App.Estacionamientos = new List<Model.Estacionamiento>();

            Model.Estacionamiento estacionamiento = new Model.Estacionamiento();
            estacionamiento.Imagen = GetBytesFromUrl("https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/drivers-loft-1-1643441566.jpg?resize=480:*");// // IMAGEN DEL LUGAR
            estacionamiento.Nombre = "Villa Auto";// // NOMBRE DEL LUGAR
            estacionamiento.Direccion = "Ayacucho 500";// // DIRECCION DEL LUGAR
            estacionamiento.TipoDeLugar = "Galpón";// // TIPO DEL LUGAR
            // LOS RANGO HORARIOS YA SE CARGARON ANTES EN EL EVENTO --> btnEditarHorario_Clicked

            //********** TEMA VEHICULOS ACEPTADOS Y SUS TARIFAS **********//

            Model.DataVehiculoAlojado dataVehiculo = new Model.DataVehiculoAlojado();
            dataVehiculo.TipoDeVehiculo = "Auto";
            dataVehiculo.CapacidadDeAlojamiento = 6;
            dataVehiculo.Tarifa_Hora = 100;
            dataVehiculo.Tarifa_Dia = 500;
            dataVehiculo.Tarifa_Semana = 3000;
            dataVehiculo.Tarifa_Mes = 10000;

            estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo); // HASTA ACA

            Model.DataVehiculoAlojado dataVehiculo1 = new Model.DataVehiculoAlojado();
            dataVehiculo1.TipoDeVehiculo = "Moto";
            dataVehiculo1.CapacidadDeAlojamiento = 10;
            dataVehiculo1.Tarifa_Hora = 25;
            dataVehiculo1.Tarifa_Dia = 50;
            dataVehiculo1.Tarifa_Semana = 300;
            dataVehiculo1.Tarifa_Mes = 1200;

            estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo1); // HASTA ACA

            Model.DataVehiculoAlojado dataVehiculo2 = new Model.DataVehiculoAlojado();
            dataVehiculo2.TipoDeVehiculo = "Camioneta";
            dataVehiculo2.CapacidadDeAlojamiento = 0;
            dataVehiculo2.Tarifa_Hora = 0;
            dataVehiculo2.Tarifa_Dia = 0;
            dataVehiculo2.Tarifa_Semana = 0;
            dataVehiculo2.Tarifa_Mes = 0;

            estacionamiento.TiposDeVehiculosAdmitidos.Add(dataVehiculo2); // HASTA ACA

            estacionamiento.MontoReserva = 115; // MONTO DE LA RESERVA

            App.Estacionamientos.Add(estacionamiento);

            //###################################################################################
            Model.Estacionamiento estacionamiento2 = new Model.Estacionamiento();
            estacionamiento2.Imagen = GetBytesFromUrl("https://i.ytimg.com/vi/K7aG4ARfaAE/maxresdefault.jpg");// // IMAGEN DEL LUGAR
            estacionamiento2.Nombre = "Aparcadero";// // NOMBRE DEL LUGAR
            estacionamiento2.Direccion = "Vicente López y Planes 150";// // DIRECCION DEL LUGAR
            estacionamiento2.TipoDeLugar = "Casa";// // TIPO DEL LUGAR
                                                  // LOS RANGO HORARIOS YA SE CARGARON ANTES EN EL EVENTO --> btnEditarHorario_Clicked

            //********** TEMA VEHICULOS ACEPTADOS Y SUS TARIFAS **********//

            Model.DataVehiculoAlojado dataVehiculo3 = new Model.DataVehiculoAlojado();
            dataVehiculo3.TipoDeVehiculo = "Auto";
            dataVehiculo3.CapacidadDeAlojamiento = 1;
            dataVehiculo3.Tarifa_Hora = 100;
            dataVehiculo3.Tarifa_Dia = 500;
            dataVehiculo3.Tarifa_Semana = 3000;
            dataVehiculo3.Tarifa_Mes = 10000;

            estacionamiento2.TiposDeVehiculosAdmitidos.Add(dataVehiculo3); // HASTA ACA

            estacionamiento2.MontoReserva = 150; // MONTO DE LA RESERVA

            App.Estacionamientos.Add(estacionamiento2);
            //###################################################################################
            Model.Estacionamiento estacionamiento3 = new Model.Estacionamiento();
            estacionamiento3.Imagen = GetBytesFromUrl("https://i.pinimg.com/originals/99/fa/fe/99fafecc92b16ec064d7fbe5fa729e8e.jpg");// // IMAGEN DEL LUGAR
            estacionamiento3.Nombre = "Cuna Auto";// // NOMBRE DEL LUGAR
            estacionamiento3.Direccion = "C. French 1550";// // DIRECCION DEL LUGAR
            estacionamiento3.TipoDeLugar = "Terreno al aire libre";// // TIPO DEL LUGAR
                                                                   // LOS RANGO HORARIOS YA SE CARGARON ANTES EN EL EVENTO --> btnEditarHorario_Clicked

            //********** TEMA VEHICULOS ACEPTADOS Y SUS TARIFAS **********//

            Model.DataVehiculoAlojado dataVehiculo4 = new Model.DataVehiculoAlojado();
            dataVehiculo4.TipoDeVehiculo = "Auto";
            dataVehiculo4.CapacidadDeAlojamiento = 10;
            dataVehiculo4.Tarifa_Hora = 100;
            dataVehiculo4.Tarifa_Dia = 500;
            dataVehiculo4.Tarifa_Semana = 3000;
            dataVehiculo4.Tarifa_Mes = 10000;

            estacionamiento3.TiposDeVehiculosAdmitidos.Add(dataVehiculo4); // HASTA ACA

            Model.DataVehiculoAlojado dataVehiculo5 = new Model.DataVehiculoAlojado();
            dataVehiculo5.TipoDeVehiculo = "Camioneta";
            dataVehiculo5.CapacidadDeAlojamiento = 10;
            dataVehiculo5.Tarifa_Hora = 200;
            dataVehiculo5.Tarifa_Dia = 1000;
            dataVehiculo5.Tarifa_Semana = 5000;
            dataVehiculo5.Tarifa_Mes = 20000;

            estacionamiento3.TiposDeVehiculosAdmitidos.Add(dataVehiculo5); // HASTA ACA

            Model.DataVehiculoAlojado dataVehiculo6 = new Model.DataVehiculoAlojado();
            dataVehiculo6.TipoDeVehiculo = "Moto";
            dataVehiculo6.CapacidadDeAlojamiento = 20;
            dataVehiculo6.Tarifa_Hora = 50;
            dataVehiculo6.Tarifa_Dia = 200;
            dataVehiculo6.Tarifa_Semana = 1000;
            dataVehiculo6.Tarifa_Mes = 5000;

            estacionamiento3.TiposDeVehiculosAdmitidos.Add(dataVehiculo6); // HASTA ACA

            estacionamiento3.MontoReserva = 0; // MONTO DE LA RESERVA

            App.Estacionamientos.Add(estacionamiento3);
            //###################################################################################

            return App.Estacionamientos;
        }


        public static List<string> TruncarLosDecimales(List<string> Lista)
        {
            return Lista;
        }

        public static decimal TruncarLosDecimales(string numero)
        {

            if (numero.Contains("."))
            {
                numero = numero.Replace(".", ",");
            }

            int indice = 0;

            char[] array = numero.ToCharArray();
            string NumeroTruncados = "";

            while (array[indice] != ',')
            {
                NumeroTruncados = NumeroTruncados + array[indice];
                indice++;
            }

            NumeroTruncados = NumeroTruncados + array[indice];
            indice++;
            NumeroTruncados = NumeroTruncados + array[indice];
            indice++;
            NumeroTruncados = NumeroTruncados + array[indice];

            decimal redondeo = System.Math.Round(Convert.ToDecimal(NumeroTruncados), 2);

            return redondeo;
        }

        public static string ExceptionMessage(Exception e)
        {
            string message = e.Message;
            while (e.InnerException != null)
            {
                e = e.InnerException;
                message = message + " -*- " + e.Message;
            };


            return message;

        }

        public static string TraduceError(Exception ex)
        {
            string errorMessage = "";

            if (ex.Message.ToUpper().Contains("FAILED TO CONNECT TO"))
            {
                errorMessage = "Se ha perdido la conexion a Internet";
            }
            else if (ex.Message.ToUpper().Contains("A TASK WAS CANCELED"))
            {
                errorMessage = "El Servidor parece no estar disponible ... \n \n Intenta mas tarde.";
            }
            else if (ex.Message.ToUpper().Contains("INTENTO DE LOGIN NO VALIDO"))
            {
                errorMessage = "Email o Contraseña NO VALIDOS";
            }
            else if (ex.Message.ToUpper().Contains("THE OPERATION WAS CANCELED"))
            {
                errorMessage = "El Servidor parece no estar disponible ... \n \n Intenta mas tarde.";
            }
            else if (ex.Message.ToUpper().Contains("SOCKET CLOSED"))
            {
                errorMessage = "El Servidor parece no estar disponible ... \n \n Intenta mas tarde.";
            }
            else
            {
                errorMessage = ex.Message;
            }

            return errorMessage;

        }


        public static string QuitarGuionBajoAEnums(string value)
        {
            return value.Replace("_", " ");
        }

        public static string AgregarGuionBajoAEnums(string value)
        {
            return value.Replace(" ", "_");
        }

        public static string PrimerLetraMayusculaRestoMinuscula(string value)
        {
            return (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower()));
        }



        public static void Messages(string Mensaje)
        {
            DependencyService.Get<IMessage>().Longtime(Mensaje);
        }
    }




}
