using WindowsInput;

namespace VoxVisio.Singletons
{
    class SharedObjectsSingleton
    {
        private static SharedObjectsSingleton _singleton;
        public readonly InputSimulator inputSimulator;

        protected SharedObjectsSingleton()
        {
            inputSimulator = new InputSimulator();
        }

        public static SharedObjectsSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new SharedObjectsSingleton();
            }

            return _singleton;
        }
    }
}
