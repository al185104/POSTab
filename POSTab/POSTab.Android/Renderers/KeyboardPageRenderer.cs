using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using POSTab.Droid;
using POSTab.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(KeyboardPageRenderer))]
namespace POSTab.Droid
{
    public class KeyboardPageRenderer : PageRenderer, TextureView.ISurfaceTextureListener
    {
        private ContentPage _page => Element as ContentPage;
        private string keys = string.Empty;

        public KeyboardPageRenderer(Context context) : base(context)
        {
            Focusable = true;
            FocusableInTouchMode = true;
        }

        protected override void OnElementChanged(
            ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (Visibility == ViewStates.Visible)
                RequestFocus();

            _page.Appearing += (sender, args) =>
            {
                RequestFocus();
            };
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            var handled = true;
            string key = keyCode.ToString();

            if (!key.Equals("Enter"))
            {
                if (key.Contains("Num"))
                    keys += key.Replace("Num", "");
                else
                    keys += key;
            }
            else
            {
                if (_page is MainPageView)
                {
                    MainPageView view = _page as MainPageView;
                    view.Scanner(keys, e.ToString());
                }
                keys = string.Empty;
            }

            return handled || base.OnKeyUp(keyCode, e);
        }

        #region Interface
        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            throw new NotImplementedException();
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}