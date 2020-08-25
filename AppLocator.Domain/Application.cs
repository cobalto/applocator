namespace AppLocator.Domain
{
    public sealed class Application
    {
        public AppId ApplicationId { get; private set; }
        public AppUrl Url { get; private set; }
        public AppPath Path { get; private set; }
        public AppMode Mode { get; private set; }

        private Application() { }

        public Application(AppId application,
            AppUrl url,
            AppPath path, 
            AppMode mode)
        {
            ApplicationId = application;
            Url = url;
            Path = path;
            Mode = mode;
        }
    }
}
