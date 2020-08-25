using System;

namespace AppLocator.Domain
{
    public sealed class AppUrl
    {
        private Uri _currentUrl;

        public AppUrl(Uri url)
        {
            _currentUrl = url;
        }

        public override string ToString()
        {
            return _currentUrl.AbsoluteUri;
        }

    }
}
