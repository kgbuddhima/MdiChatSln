// Created By : Buddhima Kudagama
// Created On : 2017-02-26
// Description : Custom render Remove borders from Entry

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using MdiChat.Android;
using MdiChat;

[assembly: ExportRenderer(typeof(NoBorderEntry), typeof(NoBorderEntryEntryRenderer))]
namespace MdiChat.Android
{
    class NoBorderEntryEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new ColorDrawable(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}