namespace AppLocator.Domain
{
    public sealed class AppMode
    {
        private AvailableMode _currentMode;
        public bool IsDebugging
        {
            get
            {
                return (_currentMode == AvailableMode.Debug);
            }
        }

        public AppMode(AvailableMode mode = AvailableMode.Debug)
        {
            _currentMode = mode;
        }

        public static implicit operator AvailableMode(AppMode mode)
        {
            return mode._currentMode;
        }

        public override string ToString()
        {
            return _currentMode.ToString();
        }
    }
}
