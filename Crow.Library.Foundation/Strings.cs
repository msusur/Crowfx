namespace Crow.Library.Foundation
{
    public struct Strings
    {


        public struct Configuration
        {
            public const string ConnectionString = "ConnectionString";
            public const string Provider = "Provider";

            public const string HostingPort = "Port";
            public const string HostingHostname = "Hostname";
            public const string HostingIsHttps = "IsHttps";
            public const string StartHost = "StartHost";

            public const string RequestContext = "RequestContext";
        }

        public struct Defaults
        {
            public const int DefaultPort = 80;
            public const string DefaultHostname = "localhost";
        }
    }
}