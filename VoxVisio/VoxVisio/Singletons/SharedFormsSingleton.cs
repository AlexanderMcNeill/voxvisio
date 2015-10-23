using VoxVisio.Screen_Overlay;

namespace VoxVisio.Singletons
{
    class SharedFormsSingleton
    {
        private static SharedFormsSingleton _singleton;

        //Form for displaying graphics over the screen
        public readonly OverlayForm overlayForm;

        //Form for the zoom click
        public readonly ZoomForm zoomForm;

        private FixationDot fixationDot;
        public readonly Toast ToastOverlay;

        protected SharedFormsSingleton()
        {
            //Setting up the forms that need to always be available to the program
            overlayForm = new OverlayForm();
            ToastOverlay = new Toast();
            overlayForm.RegisterOverlay(ToastOverlay);
            zoomForm = new ZoomForm();
            overlayForm.Show();
            fixationDot = new FixationDot();
        }

        public void EnableFixationVisualisation(bool enabled)
        {
            if (enabled)
            {
                overlayForm.RegisterOverlay(fixationDot);
            }
            else
            {
                overlayForm.RemoveOverlay(fixationDot);
            }
        }

        public static SharedFormsSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new SharedFormsSingleton();
            }

            return _singleton;
        }
    }
}
