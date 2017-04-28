using Android.App;
using Android.OS;
using Mapbox.Maps;
using Mapbox.Camera;
using Mapbox.Geometry;
using System;
using System.Diagnostics;
using Mapbox;
using Mapbox.Annotations;

namespace MapBoxTest
{
    [Activity(Label = "MapBoxTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private MapView mapView;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            MapboxAccountManager.Start(this, "MapBox Goes Here");
            //  Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            try
            {
                mapView = FindViewById<MapView>(Resource.Id.mapView);
                mapView.StyleUrl = Mapbox.Constants.Style.MapboxStreets;
                mapView.OnCreate(bundle);

                var mapboxMap = await mapView.GetMapAsync();

                var position = new CameraPosition.Builder()
                      .Target(new LatLng(41.885, -87.679)) // Sets the new camera position
                      .Zoom(11) // Sets the zoom
                      .Build(); // Creates a CameraPosition from the builder

                mapboxMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position), 3000);

                mapboxMap.AddMarker(new MarkerOptions()
                      .SetTitle("Test Marker")
                      .SetPosition(new LatLng(41.885, -87.679)));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (mapView != null)
                mapView.OnResume();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView.OnSaveInstanceState(outState);
        }
    }
}

