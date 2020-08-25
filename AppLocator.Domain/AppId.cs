namespace AppLocator.Domain
{
    public sealed class AppId
    {
        private int _currentId;

        public int Value
        {
            get
            {
                return _currentId;
            }
        }

        public AppId(int id)
        {
            _currentId = id;
        }

        public static implicit operator int(AppId appId)
        {
            return appId._currentId;
        }

        public override string ToString()
        {
            return _currentId.ToString();
        }
    }
}
