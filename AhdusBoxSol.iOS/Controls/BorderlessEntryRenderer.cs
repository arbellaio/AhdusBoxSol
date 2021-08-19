using AhdusBoxSol.iOS.Controls;
using PulseXLibraries.Controls.Borderless;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(BorderlessEntryRenderer), new[] { typeof(BorderlessEntryVisual) })]
namespace AhdusBoxSol.iOS.Controls
{
    public class BorderlessEntryRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}