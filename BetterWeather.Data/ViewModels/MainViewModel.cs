using System;
using System.IO;
using System.Net.Http;
using BetterWeather.Data.Interfaces;
using BetterWeather.Data.Services;
using ReactiveUI;
using Splat;

namespace BetterWeather.Data.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly WeatherService weatherService;
        private readonly ILocationService locationService;

        private bool showError;
        public bool ShowError
        {
            get { return showError; }
            set { this.RaiseAndSetIfChanged(ref showError, value); }
        }

        private string cityName;
        public string CityName
        {
            get { return cityName; }
            set { this.RaiseAndSetIfChanged(ref cityName, value); }
        }

        private string temperature;
        public string Temperature
        {
            get { return temperature; }
            set { this.RaiseAndSetIfChanged(ref temperature, value); }
        }

        private string humidity;
        public string Humidity
        {
            get { return humidity; }
            set { this.RaiseAndSetIfChanged(ref humidity, value); }
        }

        private IBitmap weatherIcon;
        public IBitmap WeatherIcon
        {
            get { return weatherIcon; }
            set { this.RaiseAndSetIfChanged(ref weatherIcon, value); }
        }

        public ReactiveCommand<dynamic> RefreshWeather;

        public MainViewModel()
        {
            this.weatherService = new WeatherService();
            this.locationService = Locator.CurrentMutable.GetService<ILocationService>();
            this.CityName = "Refresh to see your City";

            InitRefreshWeatherCommand();
        }

        #region Commands

        private void InitRefreshWeatherCommand()
        {
            this.RefreshWeather = ReactiveCommand.CreateAsyncTask<dynamic>(
                async _ =>
                {
                    this.ShowError = false;

                    var currentPosition = await locationService.GetCurrentPositionAsync();
                    return await this.weatherService.GetCurrentWeather(currentPosition.Lon, currentPosition.Lat);
                });

            this.RefreshWeather.Subscribe(async result =>
            {
                this.CityName = result.name;
                this.Temperature = result.main.temp + "°C";
                this.Humidity = result.main.humidity + "%";
                
                // Download Weather Icon
                HttpClient hc = new HttpClient();
                var url = string.Format("http://openweathermap.org/img/w/{0}.png", result.weather[0].icon);
                this.Log().Debug("Image URL: {0}", url);
                Stream iconDataStream = await hc.GetStreamAsync(url);
                this.WeatherIcon = await BitmapLoader.Current.Load(iconDataStream, null, null);
            });

            this.RefreshWeather.ThrownExceptions.Subscribe(ex =>
            {
                this.Log().ErrorException("RefreshWeather", ex);
                this.ShowError = true;
            });

            this.WhenAnyValue(x => x.WeatherIcon)
                .Subscribe(x => this.Log().Debug("WEATHERICON CHANGED!!!"));
        }

        #endregion
    }
}
