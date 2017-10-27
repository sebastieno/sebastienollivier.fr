namespace Blog.Functions.SearchIndexer.BindingRedirectHelper
{
    public static class ApplicationHelper
    {
        private static bool IsStarted = false;
        private static object _syncLock = new object();

        public static void Startup()
        {
            if (!IsStarted)
            {
                lock (_syncLock)
                {
                    if (!IsStarted)
                    {
                        AssemblyBindingRedirectHelper.ConfigureBindingRedirects();
                        IsStarted = true;
                    }
                }
            }
        }
    }
}
