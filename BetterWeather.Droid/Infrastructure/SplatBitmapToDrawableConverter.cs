using System;
using Android.Graphics.Drawables;
using Android.Widget;
using ReactiveUI;
using Splat;

namespace BetterWeather.Droid.Infrastructure
{
    class BitmapToImageViewConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return (fromType == typeof (IBitmap) && toType == typeof (ImageView)) ? 2 : 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            if (from == null)
            {
                result = null;
                return false;
            }

            Drawable drawable = ((IBitmap) from).ToNative();
            ImageView test = new ImageView(WeatherApp.AppContext);
            test.SetImageDrawable(drawable);

            result = test;
            return true;
        }
    }
}