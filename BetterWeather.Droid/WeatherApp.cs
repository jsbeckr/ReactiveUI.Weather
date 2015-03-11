using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using BetterWeather.Data.Interfaces;
using BetterWeather.Droid.Infrastructure;
using BetterWeather.Droid.Views;
using ReactiveUI;
using Splat;
using Weather.Droid.Infrastructure;

namespace BetterWeather.Droid
{
    [Application(Debuggable = true, ManageSpaceActivity = typeof (MainActivity))]
    public class WeatherApp : Application
    {
        public static Context AppContext;

        public WeatherApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Always useful to have the app context somewhere
            // maybe in the Locator?
            WeatherApp.AppContext = this.ApplicationContext;

            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            Locator.CurrentMutable.RegisterConstant(new BitmapToImageViewConverter(), typeof(IBindingTypeConverter));

            Locator.CurrentMutable.Register(() => new AndroidLogger(), typeof (ILogger));
            Locator.CurrentMutable.Register(() => new AndroidLocationService(), typeof(ILocationService));
        }
    }
}