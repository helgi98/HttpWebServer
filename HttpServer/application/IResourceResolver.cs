using System;

namespace HttpWebServer.application
{
    interface IResourceResolver
    {
        String Path { get; set; }
        IResource Resolve(String resourceUrl);
    }
}
