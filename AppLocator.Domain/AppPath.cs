using System;
using System.IO;

namespace AppLocator.Domain
{
    public sealed class AppPath
    {
        private string _currentPath;

        public AppPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(AppPath));

            DirectoryInfo di = new DirectoryInfo(path);

            try
            {
                if (di.Name.Length > 0)
                {
                    bool _ = true;
                }
            }
            catch(Exception ex)
            {
                throw new DomainException($"{path} is not a valid Path", ex);
            }

            if (!di.Exists)
                throw new DomainException($"{path} does not exist");

            _currentPath = di.FullName;
        }

        public override string ToString()
        {
            return _currentPath.ToString();
        }
    }
}
