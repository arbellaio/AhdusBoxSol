using AhdusBoxSol.Android.Controls;
using Android.Content;
using PulseXLibraries.Controls.Borderless;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidGraphics = Android.Graphics;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(BorderlessEntryRenderer), new[] { typeof(BorderlessEntryVisual) })]
namespace AhdusBoxSol.Android.Controls
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
            // empty
        }

        /// <summary>
        /// On element change method
        /// </summary>
        /// <param name="e">entry parametar</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(AndroidGraphics.Color.Transparent);
            Control?.SetPadding(0, 0, 0, 0);
        }
    }
}