using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EasyParking.Views.Generales
{
    public partial class Mapa : ContentPage
    {
        private readonly Geocoder _geocoder = new Geocoder();

        public Mapa()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                var result = await Geocoding.GetLocationsAsync(entry.Text);

                if (result.Any())
                    resultLocation.Text = $"lat: {result.FirstOrDefault()?.Latitude}, long: {result.FirstOrDefault()?.Longitude}";
            }
            catch (FeatureNotSupportedException notsupportedex)
            {
                // TODO: Do something useful
            }
            catch (Exception ex)
            {
                // TODO: Do something useful
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                double lat;
                double lng;

                // Watch out! You have to enter coordinates with a very
                // strict format of 11.1111,22.2222 else this will crash
                lat = Convert.ToDouble(entry.Text.Split(',')[0]);
                lng = Convert.ToDouble(entry.Text.Split(',')[1]);

                var result = await Geocoding.GetPlacemarksAsync(lat, lng);

                if (result.Any())
                    resultLocation.Text = result.FirstOrDefault()?.FeatureName;
            }
            catch (FeatureNotSupportedException notsupportedex)
            {
                // TODO: Do something useful
            }
            catch (Exception ex)
            {
                // TODO: Do something useful
            }
        }

        //async void Button_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        var result = await Geocoding.GetLocationsAsync(entry.Text);

        //        if (result.Any())
        //            resultLocation.Text = $"lat: {result.FirstOrDefault()?.Latitude}, long: {result.FirstOrDefault()?.Longitude}";
        //    }
        //    catch (FeatureNotSupportedException notsupportedex)
        //    {
        //        // TODO: Do something useful
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO: Do something useful
        //    }
        //}

        //async void Button1_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        double lat;
        //        double lng;

        //        // Watch out! You have to enter coordinates with a very
        //        // strict format of 11.1111,22.2222 else this will crash
        //        lat = Convert.ToDouble(entry.Text.Split(',')[0]);
        //        lng = Convert.ToDouble(entry.Text.Split(',')[1]);

        //        var result = await Geocoding.GetPlacemarksAsync(lat, lng);

        //        if (result.Any())
        //            resultLocation.Text = result.FirstOrDefault()?.FeatureName;
        //    }
        //    catch (FeatureNotSupportedException notsupportedex)
        //    {
        //        // TODO: Do something useful
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO: Do something useful
        //    }
        //}
    }
}
