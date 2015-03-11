using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidHUD;
using BetterWeather.Data.ViewModels;
using ReactiveUI;
using Splat;

namespace BetterWeather.Droid.Views
{
    [Activity(Label = "Weather.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ReactiveActivity<MainViewModel>
    {
        private Button refreshButton;
        private TextView cityNameTextView;
        private TextView temperatureTextView;
        private TextView humidityTextView;

        private ImageView WeatherImageView { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ActionBar.Hide();

            // Init VM
            this.ViewModel = new MainViewModel();

            // Get our button from the layout resource,
            this.refreshButton = FindViewById<Button>(Resource.Id.refreshButton);
            this.cityNameTextView = FindViewById<TextView>(Resource.Id.cityNameTextView);
            this.temperatureTextView = FindViewById<TextView>(Resource.Id.temperatureText);
            this.humidityTextView = FindViewById<TextView>(Resource.Id.humidityText);
            this.WeatherImageView = FindViewById<ImageView>(Resource.Id.weatherImageView);

            // Bind Text and Button to a VM Property and a ReactiveCommand
            this.Bind(this.ViewModel, vm => vm.CityName, v => v.cityNameTextView.Text);
            this.Bind(this.ViewModel, vm => vm.Temperature, v => v.temperatureTextView.Text);
            this.Bind(this.ViewModel, vm => vm.Humidity, v => v.humidityTextView.Text);
            this.BindCommand(this.ViewModel, vm => vm.RefreshWeather, v => v.refreshButton);

            this.WhenAnyValue(activity => activity.ViewModel.WeatherIcon)
                .Subscribe(image =>
                {
                    if (image == null)
                        return;

                    WeatherImageView.SetImageDrawable(image.ToNative());
                });

            this.WhenAnyValue(activity => activity.ViewModel.ShowError)
                .Where(x => x) // only show if ShowError is true
                .Subscribe(showError =>
                {
                    AndHUD.Shared.ShowError(this, "An error occured. We are sorrrryyy!", MaskType.Black, TimeSpan.FromSeconds(3));
                });
        }
    }
}

